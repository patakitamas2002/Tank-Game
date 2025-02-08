using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private GameObject continueButton;
    [SerializeField] TextMeshProUGUI topText;
    private List<AudioSource> audioSources;
    void Start()
    {
        audioSources = new List<AudioSource>();
        audioSources.AddRange(FindObjectsOfType<AudioSource>());
    }
    void LockPlayer()
    {
        playerInputs.playerControls.Disable();
        Cursor.lockState = CursorLockMode.None;
        pauseMenuCanvas.enabled = true;

    }
    public void PauseGame()
    {
        LockPlayer();
        // Set time scale to 0 (pause game)
        Time.timeScale = 0f;
        // Show pause menu
        audioSources.ForEach(source => source.Pause());
    }
    // Unpause game
    public void UnpauseGame()
    {
        // Set time scale to 1 (unpause game)
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        playerInputs.playerControls.Enable();
        // Hide pause menu
        pauseMenuCanvas.enabled = false;
        audioSources.ForEach(source => source.UnPause());

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

    public void LoseGame()
    {
        topText.text = "You Lost";
        continueButton.SetActive(false);
        LockPlayer();
    }
    public void WinGame()
    {
        topText.text = "You Won";
        continueButton.SetActive(false);
        LockPlayer();
    }


}
