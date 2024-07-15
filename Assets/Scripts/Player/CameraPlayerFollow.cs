using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    public Transform target;
    public float sensitivity = .5f;

    public Vector3 offset;
    public float minFov = 20f;
    public float maxFov = 40f;
    public float lerpSpeed = 0.05f;
    float fov;
    public Image crosshair;
    public Vector2 turn;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Debug.Log($"Camera {this.name} has started");
        fov = GetComponent<Camera>().fieldOfView;
    }
    void OnDisable(){
        GetComponent<AudioListener>().enabled = false;
    }
    void OnEnable(){
        //transform.parent.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        GetComponent<AudioListener>().enabled = true;
    }
    void LateUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 offsetRot = target.up *  offset.y + target.forward * offset.z;
        if(lerpSpeed == 0) transform.position = target.position + offsetRot;
        else transform.position = Vector3.SmoothDamp(transform.position, target.position + offsetRot, ref velocity, lerpSpeed);
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        target.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        fov -= Input.GetAxis("Mouse ScrollWheel") * 50;
        if(fov < minFov) fov = minFov;
        if(fov > maxFov) fov = maxFov;
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, fov, 0.05f);
        
        //GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 50;
    }
    
}
