using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public AudioSource onClick;
    public GameObject usernameUI;
    private string username;
    public TextMeshProUGUI usernameDisplay;
    public List<GameObject> levelImage;
    public List<GameObject> levelName;
    public GameObject levelNameText;
    public GameObject currentLevelImage;
    private int currentLevelIndex = 0;
    public List<string> levelSceneNames;
    

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
        onClick.Play();
        LoadAndSaveData.instance.SaveData();
        
        if(currentLevelIndex >= 0 && currentLevelIndex < levelSceneNames.Count)
        {
            string sceneNameToLoad = levelSceneNames[currentLevelIndex];
            SceneManager.LoadSceneAsync(sceneNameToLoad);
        }
    }

    private void Start()
    {
        LoadUsername();
        UpdateLevel();
        
        if (string.IsNullOrEmpty(usernameDisplay.text))
        {
            usernameUI.SetActive(true);
        }
            
        if (!string.IsNullOrEmpty(usernameDisplay.text))
        {
            usernameUI.SetActive(false);
        }
        
        lobbySound.Play();
        LoadData();
        statsMap["Health"] = stats[0];
        statsMap["Attack"] = stats[1];
        statsMap["Defense"] = stats[2];
        statsMap["Speed"] = stats[3];
        statsMap["Magnet"] = stats[4];
        
        UpdateCurrentStats();
    }

    private void Update()
    {
        statsMap["Health"].upgradeStats.text = healthUpgrade.ToString();
        statsMap["Attack"].upgradeStats.text = attackUpgrade.ToString();
        statsMap["Defense"].upgradeStats.text = defenseUpgrade.ToString();
        statsMap["Speed"].upgradeStats.text = speedUpgrade.ToString();
        statsMap["Magnet"].upgradeStats.text = magnetUpgrade.ToString();
    }

    public void Exit()
    {
        onClick.Play();
        Application.Quit();
    }

    public void UpHealth()
    {
        onClick.Play();
        healthUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void UpAttack()
    {
        onClick.Play();
        attackUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void UpDefense()
    {
        onClick.Play();
        defenseUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void UpSpeed()
    {
        onClick.Play();
        speedUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void UpMagnet()
    {
        onClick.Play();
        magnetUpgrade++;
        currentCost += upgradeCost;
        UpdateCostDisplay();
    }
    
    public void DownMagnet()
    {
        onClick.Play();
        
        if (magnetUpgrade > 0)
        {
            magnetUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }
    
    public void DownSpeed()
    {
        onClick.Play();
        
        if (speedUpgrade > 0)
        {
            speedUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }
    
    public void DownDefense()
    {
        onClick.Play();
        
        if (defenseUpgrade > 0)
        {
            defenseUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }
    
    public void DownAttack()
    {
        onClick.Play();
        
        if (attackUpgrade > 0)
        {
            attackUpgrade--;
            currentCost -= upgradeCost;
            UpdateCostDisplay();
        }
    }
    
    public void DownHealth()
    {
        onClick.Play();
        
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
        
        onClick.Play();
    }

    private void UpdateCostDisplay()
    {
        costDisplay.text = currentCost.ToString();
    }

    private void UpdateCurrentStats()
    {
        statsMap["Health"].currentStats.text = CharachterStat.instance.health.ToString();
        statsMap["Attack"].currentStats.text = CharachterStat.instance.attack.ToString();
        statsMap["Defense"].currentStats.text = CharachterStat.instance.def.ToString();
        statsMap["Speed"].currentStats.text = CharachterStat.instance.speed.ToString();
        statsMap["Magnet"].currentStats.text = CharachterStat.instance.magnet.ToString();
    }

    private void LoadData()
    { 
        LoadAndSaveData.instance.LoadData();
        health = CharachterStat.instance.health;
        attack = CharachterStat.instance.attack;
        defense = CharachterStat.instance.def;
        speed = CharachterStat.instance.speed;
        magnet = CharachterStat.instance.magnet;
    }

    public void Confirm()
    {
        usernameUI.SetActive(false);
        SaveUsername();
    }
    
    public void SaveUsernameDisplay(TMP_InputField inputField)
    {
        username = inputField.text;
        usernameDisplay.text = username;
    }

    private void SaveUsername()
    {
        PlayerPrefs.SetString("Username", usernameDisplay.text);
        PlayerPrefs.Save();
    }

    private void LoadUsername()
    {
        if (PlayerPrefs.HasKey("Username"))
        {
            usernameDisplay.text = PlayerPrefs.GetString("Username");
        }
    }

    public void NextLevel()
    {
        onClick.Play();
        
        if (currentLevelIndex < levelImage.Count - 1)
        {
            currentLevelIndex++;
            UpdateLevel();
        }
    }

    public void BackLevel()
    {
        onClick.Play();
        
        if (currentLevelIndex > 0)
        {
            currentLevelIndex--;
            UpdateLevel();
        }
    }
    
    private void UpdateLevel()
    {
        for (var i = 0; i < levelImage.Count; i++)
        {
            levelImage[i].SetActive(i == currentLevelIndex);
        }
        
        for (var i = 0; i < levelName.Count; i++)
        {
            levelName[i].SetActive(i == currentLevelIndex);
        }
    }

    public void ChangeUsername()
    {
        usernameUI.SetActive(true);
    }
}
