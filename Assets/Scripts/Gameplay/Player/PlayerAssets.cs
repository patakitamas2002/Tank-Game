using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssets : MonoBehaviour
{
    public GameObject[] cameras;
    [SerializeField] private int camIndex = 0;
    public int CamIndex { get { return camIndex; } }
    public GameObject tankObject;
    public Tank tank;
    public CameraPlayerFollow currentCamera { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        tankObject = new GameObject("Tank", typeof(Tank), typeof(Rigidbody), typeof(BoxCollider));
        tankObject.GetComponent<BoxCollider>().size = new Vector3(5, 2, 3);
        tankObject.transform.position = transform.position;
        tankObject.transform.SetParent(transform);
        tank = tankObject.GetComponent<Tank>();

        //Instantiate(new GameObject("Aimpoint"), tankObject.transform);

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i] = Instantiate(cameras[i], transform);
        }
        currentCamera = cameras[camIndex].GetComponent<CameraPlayerFollow>();
    }
    public void SwitchCamera()
    {
        int previousCam = camIndex;
        cameras[previousCam].gameObject.SetActive(false);
        camIndex += 1;

        if (camIndex >= cameras.Length)
            camIndex = 0;
        cameras[camIndex].transform.forward = cameras[previousCam].transform.forward;
        cameras[camIndex].gameObject.SetActive(true);
        currentCamera = cameras[camIndex].GetComponent<CameraPlayerFollow>();
    }



}
