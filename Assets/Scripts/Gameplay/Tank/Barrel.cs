using System;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public CannonStats stats;
    public GameObject[] shells;
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
            reload = stats.ReloadTime;
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
        if (target < -180) target += 360;
        //Debug.Log(target);
        target = Math.Clamp(target, -stats.maxElevation, stats.maxDepression);
        Quaternion trav = Quaternion.Euler(target, 0, 0);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, trav, stats.ElevationSpeed * Time.deltaTime);
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