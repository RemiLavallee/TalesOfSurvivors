using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatCondition : MonoBehaviour
{
    public GameObject GameOverUI;
    public PlayerStats player;

    private void Update()
    {
        if (player.currentHealth == 0)
        {
            GameOverUI.SetActive(true);
        }
    }

    public void Retry()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("LobbyMenu");
    }
}
