using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCondition : MonoBehaviour
{
    private EnemySpawner enemy;
    public GameObject victoryUI;

    public void Awake()
    {
        enemy = FindObjectOfType<EnemySpawner>();
    }

    public void Update()
    {
        if (enemy.enemiesAlive == 0 && enemy.currentWaveCount == enemy.waves.Count - 1)
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
