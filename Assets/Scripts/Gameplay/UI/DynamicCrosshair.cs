using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCrosshair : MonoBehaviour
{
    Transform target;
    [SerializeField] RectTransform crosshair;
    [SerializeField] LayerMask layerMask;

    void Start()
    {
        Invoke("SetTarget", 0.5f);
        crosshair = GetComponent<RectTransform>();

    }
    public void Update()
    {
        AlignCrosshair();
    }
    public void AlignCrosshair()
    {
        if (target == null) return;
        // Ray ray = new Ray(target.position, target.forward);
        if (Physics.Raycast(target.position, target.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            crosshair.position = Camera.main.WorldToScreenPoint(hit.point);
        }
        Debug.DrawRay(target.position, target.forward * 100, Color.red);
    }
    public void SetTarget()
    {
        //Debug.Log("Setting Target, parent: " + transform.parent.name);
        target = ChildFinder.FindAllByTag(transform.parent.parent, "Barrel");
    }
}
