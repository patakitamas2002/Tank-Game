using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenuCanvas;

    public void PauseGame(PlayerInputs playerInputs)
    {
        // Set time scale to 0 (pause game)
        Time.timeScale = 0f;
        // Show pause menu
        playerInputs.playerControls.Disable();
        Cursor.lockState = CursorLockMode.None;
        pauseMenuCanvas.enabled = true;
    }
    // Unpause game
    public void UnpauseGame(PlayerInputs playerInputs)
    {
        // Set time scale to 1 (unpause game)
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        playerInputs.playerControls.Enable();
        // Hide pause menu
        pauseMenuCanvas.enabled = false;
    }
    public void RestartGame()
    {
        // Load current scene (restart game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        // Load main menu scene
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
