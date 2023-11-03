using System;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager instance;
    
    public GameObject levelUpUI;
    public GameObject player;
    public bool choosingUpgrade;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LevelUp()
    {
        choosingUpgrade = true;
        levelUpUI.SetActive(true);
        Time.timeScale = 0f;
        player.SendMessage("RemoveApplyUpgrades");
    }

    public void EndLevelUp()
    {
        choosingUpgrade = false;
        levelUpUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
