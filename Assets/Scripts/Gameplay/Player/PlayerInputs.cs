
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{

    public PlayerControls playerControls;
    public PlayerAssets playerAssets;

    #region InputActions
    private InputAction moveAction;
    private InputAction fireAction;

    private InputAction cameraZoom;
    private InputAction changeCamera;
    private InputAction nextShell;
    private InputAction previousShell;

    private InputAction pauseGame;


    Vector2 mouseInput;
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.Aiming.CameraMovementX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        playerControls.Aiming.CameraMovementY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }
    private void OnEnable()
    {
        playerControls.Enable();

        moveAction = playerControls.Movement.Move;
        moveAction.Enable();

        fireAction = playerControls.Firing.Fire;
        fireAction.performed += Fire;
        fireAction.Enable();


        cameraZoom = playerControls.Aiming.CameraZoom;
        cameraZoom.performed += OnCameraZoom;
        cameraZoom.Enable();

        changeCamera = playerControls.Aiming.ChangeCamera;
        changeCamera.performed += ChangeCamera;
        changeCamera.Enable();

        nextShell = playerControls.Firing.NextShell;
        nextShell.performed += NextShell;
        nextShell.Enable();

        previousShell = playerControls.Firing.PreviousShell;
        previousShell.performed += PreviousShell;
        previousShell.Enable();

        pauseGame = playerControls.UI.PauseGame;
        pauseGame.performed += PauseGame;
        pauseGame.Enable();

    }

    private void OnDisable()
    {
        moveAction.Disable();
        fireAction.Disable();
        fireAction.performed -= Fire;
        changeCamera.Disable();
        changeCamera.performed -= ChangeCamera;
        playerControls.Disable();
        previousShell.Disable();
        nextShell.Disable();
        cameraZoom.Disable();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerAssets.tank.Accelerate(moveAction.ReadValue<Vector2>().y);
        playerAssets.tank.Rotate(moveAction.ReadValue<Vector2>().x);
        // playerAssets.tank.Brake();
    }
    void Update()
    {
        playerAssets.currentCamera.Aim(mouseInput);
        // Debug.Log(mouseInput);
    }



    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired");
        playerAssets.tank.barrel.Fire();

    }
    private void ChangeCamera(InputAction.CallbackContext context)
    {
        playerAssets.SwitchCamera();
    }

    private void OnCameraZoom(InputAction.CallbackContext context)
    {
        float zoom = context.ReadValue<float>();
        //Debug.Log(zoom);
        playerAssets.cameras[playerAssets.CamIndex].GetComponent<CameraPlayerFollow>().Zoom(zoom);
    }
    // private void OnCameraMovement(InputAction.CallbackContext context)
    // {
    //     Vector2 input = context.ReadValue<Vector2>();
    //     // Debug.Log(input);
    //     playerAssets.cameras[playerAssets.CamIndex].GetComponent<CameraPlayerFollow>().Aim(input);
    // }
    private void NextShell(InputAction.CallbackContext context)
    {
        playerAssets.tank.barrel.NextShell();
    }
    private void PreviousShell(InputAction.CallbackContext context)
    {
        playerAssets.tank.barrel.PreviousShell();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        playerAssets.pauseMenu.GetComponent<PauseMenu>().PauseGame(this);
    }
}
