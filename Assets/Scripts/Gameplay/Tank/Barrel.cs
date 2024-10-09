using System;
using UnityEngine;

[System.Serializable]
public class Barrel : MonoBehaviour
{
    public GameObject Model;
    public string Name;
    public float Caliber;
    public float ReloadTime;
    public float ElevationSpeed;
    public float maxElevation = 15f;
    public float maxDepression = 10f;
    public float rotationSpeed = 5.0f;
    public Bullet[] shells;
    private int currentShell = 0;
    private float reload = 0;

    void Update()
    {

        if (reload > 0f)
        {
            reload -= Time.deltaTime;
        }

    }
    public void Fire()
    {
        if (reload <= 0f)
        {
            Instantiate(shells[currentShell], transform.position, transform.rotation);
            reload = ReloadTime;
        }
    }


    public void NextShell()
    {
        currentShell = (currentShell + 1) % shells.Length;
        Debug.Log("Selected shell: " + shells[currentShell].name);
    }

    public void PreviousShell()
    {
        currentShell = (currentShell - 1 + shells.Length) % shells.Length;
        Debug.Log("Selected shell: " + shells[currentShell].name);
    }

    public void Elevate(Transform aimPoint)
    {
        Vector3 targetDirection = aimPoint.position - transform.position;
        float range = Mathf.Sqrt(targetDirection.x * targetDirection.x + targetDirection.z * targetDirection.z);
        float target = Mathf.Atan2(-targetDirection.y, range) * Mathf.Rad2Deg - transform.parent.eulerAngles.x;
        target = Math.Clamp(target, -maxElevation, maxDepression);
        Quaternion trav = Quaternion.Euler(target, 0, 0);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, trav, rotationSpeed);
    }

    public void AlignCrosshair(RectTransform crosshair)
    {
        Ray ray = new Ray(transform.GetChild(0).position, transform.GetChild(0).forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            crosshair.position = Camera.main.WorldToScreenPoint(hit.point);
        }
        Debug.DrawRay(transform.GetChild(0).position, transform.GetChild(0).forward * 100, Color.red);
    }
}