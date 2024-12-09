using System;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public CannonStats stats;
    public GameObject[] shells;
    private int currentShell = 0;
    private float reload = 0;
    private float lengthOffset;

    void Start()
    {
        lengthOffset = GetLengthOffset();
    }

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
            // Debug.Log(lengthOffset);
            Instantiate(shells[currentShell], transform.position + (lengthOffset + 1) * transform.forward, transform.rotation);
            // Time.timeScale = 0.05f;
            reload = stats.ReloadTime;
            Debug.Log("Fired");

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
        target = Math.Clamp(target, -stats.maxElevation, stats.maxDepression);
        Quaternion trav = Quaternion.Euler(target, 0, 0);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, trav, stats.ElevationSpeed * Time.deltaTime);
    }


    float GetLengthOffset()
    {
        float length = 0f;
        foreach (Renderer child in transform.GetComponentsInChildren<Renderer>())
        {
            length += child.bounds.size.z;
        }
        return length;
    }
    // public void AlignCrosshair(RectTransform crosshair)
    // {
    //     Ray ray = new Ray(transform.position, transform.forward);
    //     if (Physics.Raycast(ray, out RaycastHit hit))
    //     {
    //         crosshair.position = Camera.main.WorldToScreenPoint(hit.point);
    //     }
    //     Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
    // }
}