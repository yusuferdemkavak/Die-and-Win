using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelector;
    public GameObject settingsMenu;

    public void OnClickLevels()
    {
        mainMenu.SetActive(false);
        levelSelector.SetActive(true);
    }

    public void OnClickSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickBackInLevelSelector()
    {
        levelSelector.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnClickBackInSettingsMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnClickLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
