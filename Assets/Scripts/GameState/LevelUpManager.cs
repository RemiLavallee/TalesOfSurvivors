using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{

    private PlayerStats player;
    public GameObject levelUpUI;

    [Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI upgradeName;
        public TextMeshProUGUI upgradeDescription;
        public Image upgradeImage;
        public Button buttonConfirm;
    }

    [Serializable]
    public class UpgradeItem
    {
        public GameObject item;
        public SkillScriptableObject itemData;
    }

    public List<UpgradeItem> upgradeItems = new List<UpgradeItem>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();
    
    public void LevelUp()
    {
            levelUpUI.SetActive(true);
            Time.timeScale = 0f;
    }

    public void EndLevelUp()
    {
        levelUpUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpgradeOptions()
    {
        
    }
    
}
