using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCondition : MonoBehaviour
{
    public EnemySpawner enemy;
    public GameObject victoryUI;

    public void Update()
    {
        if (enemy.enemiesAlive == -1)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
            victoryUI.SetActive(true);
            Time.timeScale = 0;
    }

    public void Confirm()
    {
        SceneManager.LoadScene("LobbyMenu");
    }
}
