using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    
    public List<WeaponController> weaponSlots = new List<WeaponController>();
    public List<PassiveItems> passiveItemsSlots = new List<PassiveItems>();
    public int[] weaponLevels = new int[5];
    public int[] passiveItemLevels = new int[5];

    [Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public SkillScriptableObject weaponData;
    }

    [Serializable]
    public class PassiveItemsUpgrade
    {
        public int passiveItemUpgradeIndex;
        public GameObject initialPassiveItem;
        public PassiveItemScriptableObject passiveItemData;
    }

    [Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI upgradeName;
        public TextMeshProUGUI upgradeDescription;
        public Image upgradeImage;
        public Button upgradeButton;
        public GameObject panel;
        public TextMeshProUGUI newUpgradeName;
        public GameObject newWeaponPanel;
        public GameObject weaponPanel;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemsUpgrade> passiveUpgradeOptions = new List<PassiveItemsUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    public PlayerStats player;

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
    
    private void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        
        if (LevelUpManager.instance != null && LevelUpManager.instance.choosingUpgrade)
        {
            LevelUpManager.instance.EndLevelUp();
        }
     
    }
    
    public void AddPassiveItem(int slotIndex, PassiveItems passiveItems)
    {
        passiveItemsSlots[slotIndex] = passiveItems;
        passiveItemLevels[slotIndex] = passiveItems.passiveItemData.Level;
        
        if (LevelUpManager.instance != null && LevelUpManager.instance.choosingUpgrade)
        {
            LevelUpManager.instance.EndLevelUp();
        }
    }

    private void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            var weapon = weaponSlots[slotIndex];

            var upgradeWeapon =
                Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradeWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradeWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradeWeapon.GetComponent<WeaponController>().weaponData.Level;

            weaponUpgradeOptions[upgradeIndex].weaponData = upgradeWeapon.GetComponent<WeaponController>().weaponData;
            
            if (LevelUpManager.instance != null && LevelUpManager.instance.choosingUpgrade)
            {
                LevelUpManager.instance.EndLevelUp();
            }
        }
    }

    private void LevelUpPassiveItem(int slotIndex, int upgradeIndex)
    {
        if (passiveItemsSlots.Count > slotIndex)
        {
            var passiveItem = passiveItemsSlots[slotIndex];

            var upgradePassiveItem =
                Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradePassiveItem.transform.SetParent(transform);
            AddPassiveItem(slotIndex, upgradePassiveItem.GetComponent<PassiveItems>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[slotIndex] = upgradePassiveItem.GetComponent<PassiveItems>().passiveItemData.Level;

            passiveUpgradeOptions[upgradeIndex].passiveItemData =
                upgradePassiveItem.GetComponent<PassiveItems>().passiveItemData;
            
            if (LevelUpManager.instance != null && LevelUpManager.instance.choosingUpgrade)
            {
                LevelUpManager.instance.EndLevelUp();
            }
        }
    }
    
    private void ApplyUpgradeOptions()
    {
        var availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        var availablePassiveItemsUpgrades = new List<PassiveItemsUpgrade>(passiveUpgradeOptions);
        
        foreach (var upgradeOption in upgradeUIOptions)
        {
            
            if (availableWeaponUpgrades.Count == 0 && availablePassiveItemsUpgrades.Count == 0)
            {
                return;
            }

            int upgradeType;

            if (availableWeaponUpgrades.Count == 0)
            {
                upgradeType = 2;
            }
            else if(availablePassiveItemsUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                upgradeType = Random.Range(1, 3);
            }
            
            switch (upgradeType)
            {
                case 1:
                    AssignWeaponUpgrade(upgradeOption,  availableWeaponUpgrades);
                    break;
                case 2:
                    AssignPassiveItemUpgrade(upgradeOption,  availablePassiveItemsUpgrades);
                    break;
            }
        }
    }

    private void AssignWeaponUpgrade(UpgradeUI upgradeOption,  List<WeaponUpgrade> availableWeaponUpgrades)
    {
        var chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];
        availableWeaponUpgrades.Remove(chosenWeaponUpgrade);

        if (chosenWeaponUpgrade == null) return;
        EnableUpgradeUI(upgradeOption);
            
        var existingWeaponIndex = -1; 
        for (var i = 0; i < weaponSlots.Count; i++)
        {
            if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
            {
                existingWeaponIndex = i; 
                break; 
            }
        }
        if (existingWeaponIndex != -1)
        {
            UpgradeWeaponUI(upgradeOption, chosenWeaponUpgrade, existingWeaponIndex);
        }
        else 
        {
            NewWeaponUI(upgradeOption, chosenWeaponUpgrade);
        }
    }
    
    private void AssignPassiveItemUpgrade(UpgradeUI upgradeOption,  List<PassiveItemsUpgrade> availablePassiveItemsUpgrades)
    {
        var chosenPassiveItemsUpgrade = availablePassiveItemsUpgrades[Random.Range(0, availablePassiveItemsUpgrades.Count)];
        availablePassiveItemsUpgrades.Remove(chosenPassiveItemsUpgrade);

        if (chosenPassiveItemsUpgrade == null) return;
        EnableUpgradeUI(upgradeOption);
            
        var existingItemIndex = -1; 
        for (var i = 0; i < passiveItemsSlots.Count; i++)
        {
            if (passiveItemsSlots[i] != null && passiveItemsSlots[i].passiveItemData == chosenPassiveItemsUpgrade.passiveItemData)
            {
                existingItemIndex = i; 
                break;
            }
        }
            
        if (existingItemIndex != -1)
        {
            UpgradePassiveItemUI(upgradeOption, chosenPassiveItemsUpgrade, existingItemIndex);
        }
        else 
        {
            NewPassiveItemUI(upgradeOption, chosenPassiveItemsUpgrade);
        }
    }
    
    private void RemoveUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
        }
    }

    public void RemoveApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    private void NewWeaponUI(UpgradeUI upgradeOption, WeaponUpgrade chosenWeaponUpgrade)
    {
        upgradeOption.newWeaponPanel.SetActive(true);
        upgradeOption.weaponPanel.SetActive(false);
        
        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));
        upgradeOption.upgradeDescription.text = chosenWeaponUpgrade.weaponData.Description;
        upgradeOption.newUpgradeName.text = chosenWeaponUpgrade.weaponData.Name;
        upgradeOption.upgradeImage.sprite = chosenWeaponUpgrade.weaponData.Image;
    }

    private void UpgradeWeaponUI(UpgradeUI upgradeOption, WeaponUpgrade chosenWeaponUpgrade, int existingWeaponIndex)
    {
        if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab)
        {
            DisableUpgradeUI(upgradeOption);
            return;
        }
        
        upgradeOption.newWeaponPanel.SetActive(false);
        upgradeOption.weaponPanel.SetActive(true);
        
        upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(existingWeaponIndex, chosenWeaponUpgrade.weaponUpgradeIndex));
        upgradeOption.upgradeDescription.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
        upgradeOption.upgradeName.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
        upgradeOption.upgradeImage.sprite = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Image;
    }

    private void NewPassiveItemUI(UpgradeUI upgradeOption, PassiveItemsUpgrade chosenPassiveItemsUpgrade)
    {
        upgradeOption.newWeaponPanel.SetActive(true);
        upgradeOption.weaponPanel.SetActive(false);
        
        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemsUpgrade.initialPassiveItem));
        upgradeOption.upgradeDescription.text = chosenPassiveItemsUpgrade.passiveItemData.Description;
        upgradeOption.newUpgradeName.text = chosenPassiveItemsUpgrade.passiveItemData.Name;
        upgradeOption.upgradeImage.sprite = chosenPassiveItemsUpgrade.passiveItemData.Image;
    }

    private void UpgradePassiveItemUI(UpgradeUI upgradeOption, PassiveItemsUpgrade chosenPassiveItemsUpgrade, int existingItemIndex)
    {
        if (!chosenPassiveItemsUpgrade.passiveItemData.NextLevelPrefab)
        {
            DisableUpgradeUI(upgradeOption);
            return;
        }
        
        upgradeOption.newWeaponPanel.SetActive(false);
        upgradeOption.weaponPanel.SetActive(true);
        
        upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(existingItemIndex, chosenPassiveItemsUpgrade.passiveItemUpgradeIndex));
        upgradeOption.upgradeDescription.text = chosenPassiveItemsUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItems>().passiveItemData.Description;
        upgradeOption.upgradeName.text = chosenPassiveItemsUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItems>().passiveItemData.Name;
        upgradeOption.upgradeImage.sprite = chosenPassiveItemsUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItems>().passiveItemData.Image;
    }

    private static void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.panel.gameObject.SetActive(false);
    }
    
    private static void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.panel.gameObject.SetActive(true);
    }

    public bool AllPanelsDisabled()
    {
        foreach (var upgradeUI in upgradeUIOptions)
        {
            if (upgradeUI.panel.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
}
