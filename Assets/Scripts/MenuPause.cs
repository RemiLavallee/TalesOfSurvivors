using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject pauseUI;
    public static bool gamePause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }
    }

    void Paused()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        gamePause = true;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        gamePause = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("LobbyMenu");
    }
}
