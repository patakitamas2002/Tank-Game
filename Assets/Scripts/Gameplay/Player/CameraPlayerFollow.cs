using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPlayerFollow : MonoBehaviour
{
    public CameraType cameraType;
    public InputAction inputSystem;
    public Transform target;
    public float sensitivity = 1f;

    public Vector3 offset;
    [SerializeField] float minFov = 20f;
    [SerializeField] float maxFov = 40f;
    float fov;
    public RectTransform crosshair;
    public float turnX, turnY;

    void Start()
    {
        switch (cameraType)
        {
            case CameraType.FirstPerson:
                sensitivity = PlayerSettings.FPSensitivity;
                break;
            case CameraType.ThirdPerson:
                sensitivity = PlayerSettings.TPSensitivity;
                break;
        }
        sensitivity = PlayerSettings.FPSensitivity;
        Cursor.lockState = CursorLockMode.Locked;
        fov = GetComponent<Camera>().fieldOfView;
        FindTarget();
    }
    void OnDisable()
    {
        GetComponent<AudioListener>().enabled = false;

    }
    void OnEnable()
    {
        GetComponent<AudioListener>().enabled = true;

    }
    void Update()
    {
        target.rotation = Quaternion.Euler(-turnY, turnX, 0);
        transform.localRotation = Quaternion.Euler(-turnY, turnX, 0);
    }
    void FixedUpdate()
    {
        Vector3 offsetRot = target.up * offset.y + target.forward * offset.z;
        transform.position = target.position + offsetRot;
    }
    public void Aim(Vector2 value)
    {
        turnX += value.x * sensitivity;
        turnY += value.y * sensitivity;
        // turnX += value.x;
        // turnY += value.y;
        // Debug.Log(turnX + ", " + turnY); 
        turnY = Math.Clamp(turnY, -90, 90);
    }
    public void Zoom(float value)
    {
        fov -= value * 50;
        if (fov < minFov) fov = minFov;
        if (fov > maxFov) fov = maxFov;
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, fov, 0.05f);
    }

    public void FindTarget()
    {
        if (cameraType == CameraType.FirstPerson)
        {
            target = ChildFinder.FindAllByName(transform.parent, "FPCamera");
            if (target == null) Debug.Log("FPCamera not found");
        }
        else
        {
            target = ChildFinder.FindAllByName(transform.parent, "TPCamera");
            if (target == null) Debug.Log("TPCamera not found");
        }
        Debug.Log(target.name);
    }
}

public enum CameraType
{
    FirstPerson,
    ThirdPerson
}
