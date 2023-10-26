using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatCondition : MonoBehaviour
{
    public GameObject GameOverUI;
    public PlayerStats player;
    public GameObject xpBarUI;

    private void Update()
    {
        if (player.currentHealth == 0)
        {
            DestroyAllEnemies();
            StartCoroutine(ActiveUIDelay(0.5f));
            GameOverUI.SetActive(true);
            xpBarUI.SetActive(false);
        }
    }
    
    private void DestroyAllEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }
    
    private IEnumerator ActiveUIDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
       
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
