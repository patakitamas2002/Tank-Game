using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public PlayerControls playerControls;
    public GameObject tankObject;
    public Tank tank;

    #region InputActions
    private InputAction moveAction;
    private InputAction turnAction;
    private InputAction fireAction;


    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {

        moveAction = playerControls.Movement.ForwardBackward;
        moveAction.performed += context => tank.Accelerate(context.ReadValue<float>());

        fireAction = playerControls.Firing.Fire;
        fireAction.performed += Fire;
        fireAction.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    void Start()
    {
        tankObject = new GameObject("Tank", typeof(Tank), typeof(Rigidbody));
        tankObject.transform.position = transform.position;
        tankObject.transform.SetParent(transform);
        tank = tankObject.GetComponent<Tank>();
        Instantiate(new GameObject("Aimpoint"), tankObject.transform);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired");
        tank.barrel.Fire();
    }

}
