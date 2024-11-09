using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCrosshair : MonoBehaviour
{
    Transform target;
    [SerializeField] RectTransform crosshair;

    void Start()
    {
        Invoke("SetTarget", 3f);
        crosshair = GetComponent<RectTransform>();

    }
    public void Update()
    {
        AlignCrosshair();
    }
    public void AlignCrosshair()
    {
        if (target == null) return;
        Ray ray = new Ray(target.GetChild(0).position, target.GetChild(0).forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            crosshair.position = Camera.main.WorldToScreenPoint(hit.point);
        }
        Debug.DrawRay(target.GetChild(0).position, target.GetChild(0).forward * 100, Color.red);
    }
    public void SetTarget()
    {
        //Debug.Log("Setting Target, parent: " + transform.parent.name);
        target = ChildFinder.FindAllByTag(transform.parent.parent, "Barrel");
    }
}
