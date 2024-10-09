using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject tankObject;
    public GameObject aim;

    public RectTransform crosshair;
    public Camera[] cameras;
    int camIndex;

    Tank tank;
    void Start()
    {
        tank = tankObject.GetComponent<Tank>();
        camIndex = 0;
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
            cameras[camIndex].GetComponent<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();

        }
        Aim(cameras[camIndex]);


        tank.barrel.GetComponent<Barrel>().AlignCrosshair(crosshair);
    }
    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0)
            tank.Accelerate(Input.GetAxis("Vertical"));
        if (Input.GetAxis("Horizontal") != 0)
            tank.Rotate(Input.GetAxis("Horizontal"));
        if (Input.GetKey(KeyCode.Space))
            tank.Brake();
    }
    void Aim(Camera cam)
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            aim.transform.position = hit.point;
        }
        // if(Physics.Raycast(ray, cam.transform.forward, out RaycastHit hit)){
        //     aim.transform.position = hit.point;
        // }
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red);

    }
    void SwitchCamera()
    {
        int previousCam = camIndex;
        cameras[camIndex].enabled = false;
        camIndex += 1;

        if (camIndex >= cameras.Length)
            camIndex = 0;
        cameras[camIndex].transform.forward = cameras[previousCam].transform.forward;
        // cameras[camIndex].transform.parent.LookAt(aim.transform);
        // cameras[camIndex].transform.parent.rotation = Quaternion.Euler(cameras[camIndex].transform.rotation.x, cameras[camIndex].transform.rotation.y, 0);
        cameras[camIndex].enabled = true;
    }
}
