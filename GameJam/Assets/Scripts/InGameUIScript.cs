using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIScript : MonoBehaviour
{
    public Explosion explosion;
    public bool isOver;
    public GameObject endScreen;
    public GameObject pauseMenu;

    public void Update()
    {
        if (isOver)
        {
            StartCoroutine(GameOver());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickResume()
    {
        pauseMenu.SetActive(false);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(2f);
        endScreen.SetActive(true);
    }
}
