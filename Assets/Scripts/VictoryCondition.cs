using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCondition : MonoBehaviour
{
    public EnemySpawner enemy;
    public GameObject victoryUI;

    public void Update()
    {
        if (enemy.isLastWaveCompleted && enemy.enemiesAlive == -1)
        {
            LevelComplete();
        }
        
    }

    public void LevelComplete()
    {
            victoryUI.SetActive(true);
            Time.timeScale = 0;
    }

    public void Confirm()
    {
        LoadAndSaveData.instance.SaveData();
        SceneManager.LoadScene("LobbyMenu");
    }
}
