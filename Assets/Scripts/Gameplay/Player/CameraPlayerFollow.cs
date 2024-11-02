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
    private void Update()
    {

        Vector3 offsetRot = target.up * offset.y + target.forward * offset.z;
        transform.position = target.position + offsetRot;

        target.rotation = Quaternion.Euler(-turnY, turnX, 0);
        transform.localRotation = Quaternion.Euler(-turnY, turnX, 0);
    }
    public void Aim(Vector2 value)
    {
        turnX += value.x * sensitivity;
        turnY += value.y * sensitivity;
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
            target = GameObject.Find("FPCamera").transform;
            if (target == null) Debug.Log("FPCamera not found");
        }
        else
        {
            target = GameObject.Find("TPCamera").transform;
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
