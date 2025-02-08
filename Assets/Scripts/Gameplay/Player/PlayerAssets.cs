using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAssets : MonoBehaviour
{
    public GameObject[] cameras;
    public CameraPlayerFollow[] testCameras;
    public PauseMenu pauseMenu;
    [SerializeField] private int camIndex = 0;
    public int CamIndex { get { return camIndex; } }
    public GameObject tankObject;
    public Tank tank;
    public CameraPlayerFollow currentCamera { get; private set; }
    Transform aimPoint;
    [SerializeField] private GameState gameState;

    // Start is called before the first frame update
    void Awake()
    {
        tankObject = Tank.CreateTank(GameOptions.hull.Model, GameOptions.turret.Model, GameOptions.barrel.Model, transform);
        // tankObject = new GameObject("Tank", );
        // tankObject.GetComponent<BoxCollider>().size = new Vector3(5, 2, 3);
        // tankObject.transform.position = transform.position;
        tankObject.transform.SetParent(transform);
        tank = tankObject.GetComponent<Tank>();
        foreach (Renderer gameObject in tankObject.GetComponentsInChildren<Renderer>())
        {
            gameObject.gameObject.layer = LayerMask.NameToLayer("PlayerTank");
        }
        foreach (Armor armor in tankObject.GetComponentsInChildren<Armor>())
        {
            armor.gameObject.layer = LayerMask.NameToLayer("PlayerTank");
        }

        //aimPoint = Instantiate(new GameObject("Aimpoint"), transform).transform;
        aimPoint = new GameObject("Aimpoint").transform;
        aimPoint.SetParent(transform);

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i] = Instantiate(cameras[i], transform);
        }
        currentCamera = cameras[camIndex].GetComponent<CameraPlayerFollow>();
        currentCamera.transform.tag = "MainCamera";
    }

    public void SwitchCamera()
    {
        int previousCam = camIndex;
        cameras[previousCam].gameObject.SetActive(false);
        cameras[previousCam].tag = "Untagged";
        camIndex += 1;

        if (camIndex >= cameras.Length)
            camIndex = 0;
        cameras[camIndex].transform.forward = cameras[previousCam].transform.forward;
        cameras[camIndex].gameObject.SetActive(true);
        currentCamera = cameras[camIndex].GetComponent<CameraPlayerFollow>();
        currentCamera.transform.tag = "MainCamera";
    }
    void Update()
    {
        SetAimPointPosition();
        tank.turret.RotateTowards(aimPoint);
        tank.barrel.Elevate(aimPoint);
        if (tank.isDead) gameState.LoseGame();
    }
    void SetAimPointPosition()
    {
        //  Debug.Log(Camera.main.name);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            aimPoint.position = hit.point;
        }

    }

}
