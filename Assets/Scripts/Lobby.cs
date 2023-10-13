using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class Lobby : MonoBehaviour
{
    [SerializeField] private AudioSource lobbySound;
    public int healthUpgrade;
    public int attackUpgrade;
    public int defenseUpgrade;
    public int speedUpgrade;
    public int magnetUpgrade;
    public int attack;
    public int defense;
    public int speed;
    public int magnet;
    public int health;
    public int upgradeCost = 500;
    public int currentCost = 0;
    public TextMeshProUGUI costDisplay; 

    [Serializable]
    public class Stats
    {
        public string nameStat;
        public TextMeshProUGUI upgradeStats;
        public TextMeshProUGUI currentStats;
        public Button upStat;
        public Button downStat;
    }
    
    public List<Stats> stats;
    public Dictionary<string, Stats> statsMap = new Dictionary<string, Stats>();
    
    public void Play()
    {
        LoadAndSaveData.instance.SaveData();
        SceneManager.LoadSceneAsync("Level1");
    }

    public void Start()
    {
        lobbySound.Play();
        LoadData();
        statsMap["Health"] = stats[0];
        statsMap["Attack"] = stats[1];
        statsMap["Defense"] = stats[2];
        statsMap["Speed"] = stats[3];
        statsMap["Magnet"] = stats[4];
        
        UpdateCurrentStats();
        
    }

    public void Update()
    {
        statsMap["Health"].upgradeStats.text = healthUpgrade.ToString();
        statsMap["Attack"].upgradeStats.text = attackUpgrade.ToString();
        statsMap["Defense"].upgradeStats.text = defenseUpgrade.ToString();
        statsMap["Speed"].upgradeStats.text = speedUpgrade.ToString();
        statsMap["Magnet"].upgradeStats.text = magnetUpgrade.ToString();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpHealth()
    {
        healthUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void UpAttack()
    {
        attackUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void UpDefense()
    {
        defenseUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void UpSpeed()
    {
        speedUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void UpMagnet()
    {
        magnetUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void DownMagnet()
    {
        if (magnetUpgrade > 0)
        {
            magnetUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }
    
    public void DownSpeed()
    {
        if (speedUpgrade > 0)
        {
            speedUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }
    
    public void DownDefense()
    {
        if (defenseUpgrade > 0)
        {
            defenseUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }
    
    public void DownAttack()
    {
        if (attackUpgrade > 0)
        {
            attackUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }
    
    public void DownHealth()
    {
        if (healthUpgrade > 0)
        {
            healthUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }

    public void BuyStats()
    {
        if (Inventory.instance.coinsCount >= currentCost)
        {
            Inventory.instance.RemoveCoins(currentCost);
        
            health += healthUpgrade;
            attack += attackUpgrade;
            defense += defenseUpgrade;
            speed += speedUpgrade;
            magnet += magnetUpgrade;

            healthUpgrade = 0;
            attackUpgrade = 0;
            defenseUpgrade = 0;
            speedUpgrade = 0;
            magnetUpgrade = 0;

            statsMap["Health"].currentStats.text = health.ToString();
            statsMap["Attack"].currentStats.text = attack.ToString();
            statsMap["Defense"].currentStats.text = defense.ToString();
            statsMap["Speed"].currentStats.text = speed.ToString();
            statsMap["Magnet"].currentStats.text = magnet.ToString();
            
            currentCost = 0;
            UpdateCostDisplay();
        }
        
        CharachterStat.instance.health = health;
        CharachterStat.instance.attack = attack;
        CharachterStat.instance.def = defense;
        CharachterStat.instance.speed = speed;
        CharachterStat.instance.magnet = magnet;
    }
    
    public void UpdateCostDisplay()
    {
        costDisplay.text = currentCost.ToString();
    }
    
    public void UpdateCurrentStats()
    {
        statsMap["Health"].currentStats.text = CharachterStat.instance.health.ToString();
        statsMap["Attack"].currentStats.text = CharachterStat.instance.attack.ToString();
        statsMap["Defense"].currentStats.text = CharachterStat.instance.def.ToString();
        statsMap["Speed"].currentStats.text = CharachterStat.instance.speed.ToString();
        statsMap["Magnet"].currentStats.text = CharachterStat.instance.magnet.ToString();
    }
    
    public void LoadData()
    { 
        LoadAndSaveData.instance.LoadData();
        health = CharachterStat.instance.health;
        attack = CharachterStat.instance.attack;
        defense = CharachterStat.instance.def;
        speed = CharachterStat.instance.speed;
        magnet = CharachterStat.instance.magnet;
    }
}
