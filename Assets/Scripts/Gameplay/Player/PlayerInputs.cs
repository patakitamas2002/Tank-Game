using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInputs : MonoBehaviour
{
    public PlayerControls playerControls;
    public GameObject tankObject;
    public Tank tank;

    #region InputActions
    float acceleration = 0;
    float turn = 0;
    private InputAction moveAction;
    private InputAction fireAction;


    private InputAction changeCamera;
    public Camera[] cameras;
    private int camIndex = 0;

    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {

        moveAction = playerControls.Movement.Move;
        moveAction.Enable();

        fireAction = playerControls.Firing.Fire;
        fireAction.performed += Fire;
        fireAction.Enable();


        changeCamera = playerControls.Aiming.ChangeCamera;
        changeCamera.performed += ChangeCamera;
        changeCamera.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        fireAction.Disable();
        fireAction.performed -= Fire;
        changeCamera.Disable();
        changeCamera.performed -= ChangeCamera;
        playerControls.Disable();

    }
    void Start()
    {
        tankObject = new GameObject("Tank", typeof(Tank), typeof(Rigidbody), typeof(BoxCollider));
        tankObject.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);
        tankObject.transform.position = transform.position;
        tankObject.transform.SetParent(transform);
        tank = tankObject.GetComponent<Tank>();
        Instantiate(new GameObject("Aimpoint"), tankObject.transform);

    }

    // Update is called once per frame
    void Update()
    {

        acceleration = moveAction.ReadValue<Vector2>().x;
        turn = moveAction.ReadValue<Vector2>().y;
        Debug.Log("acceleration: " + acceleration + " turn: " + turn);
        tank.Accelerate(acceleration);
        tank.Rotate(turn);
    }
    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired");
        tank.barrel.Fire();

    }
    private void ChangeCamera(InputAction.CallbackContext context)
    {
        int previousCam = camIndex;
        cameras[camIndex].enabled = false;
        camIndex += 1;

        if (camIndex >= cameras.Length)
            camIndex = 0;
        cameras[camIndex].transform.forward = cameras[previousCam].transform.forward;
        cameras[camIndex].enabled = true;
    }

}
