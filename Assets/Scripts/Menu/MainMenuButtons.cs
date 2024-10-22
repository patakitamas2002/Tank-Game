using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{

    private GameObject currentMenu;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        SwitchMenu(transform.GetChild(0).gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void SwitchMenu(GameObject menu)
    {
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
        }
        currentMenu = menu;
        menu.SetActive(true);
    }
}
