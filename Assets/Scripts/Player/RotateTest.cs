using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
        //Assign a GameObject in the Inspector to rotate around
    public GameObject target;

    void Update()
    {
        //Debug.Log($"target: {target.transform.eulerAngles}, transform: {transform.eulerAngles}");
        // if(target.transform.eulerAngles.y != transform.rotation.eulerAngles.y)
        //     transform.RotateAround(target.transform.position, Vector3.up, 180 * Time.deltaTime);
    }
}
