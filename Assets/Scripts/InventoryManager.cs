using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class InventoryManager : MonoBehaviour
{

    public List<WeaponController> weaponSlots = new List<WeaponController>();
    public List<PassiveItems> passiveItemsSlots = new List<PassiveItems>();
    public int[] weaponLevels = new int[5];
    public int[] passiveItemLevels = new int[5];

    [Serializable]
    public class WeaponUpgrade
    {
        public GameObject initialWeapon;
        public SkillScriptableObject weaponData;
    }

    [Serializable]
    public class PassiveItemsUpgrade
    {
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
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemsUpgrade> passiveUpgradeOptions = new List<PassiveItemsUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    public PlayerStats player;
    private LevelUpManager levelUp;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
        levelUp = GetComponent<LevelUpManager>();
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        if (levelUp != null && levelUp.choosingUpgrade)
        {
            levelUp.EndLevelUp(); 
        }
     
    }
    
    public void AddPassiveItem(int slotIndex, PassiveItems passiveItems)
    {
        passiveItemsSlots[slotIndex] = passiveItems;
        passiveItemLevels[slotIndex] = passiveItems.passiveItemData.Level;
        if (levelUp != null && levelUp.choosingUpgrade)
        {
            levelUp.EndLevelUp(); 
        }
    }

    private void LevelUpWeapon(int slotIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            var weapon = weaponSlots[slotIndex];
            if (!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogError("Next Level doesnt exist");
                return;
            }
            var upgradeWeapon =
                Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            AddWeapon(slotIndex, upgradeWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradeWeapon.GetComponent<WeaponController>().weaponData.Level;
            if (levelUp != null && levelUp.choosingUpgrade)
            {
                levelUp.EndLevelUp(); 
            }
        }
    }

    private void LevelUpPassiveItem(int slotIndex)
    {
        if (passiveItemsSlots.Count > slotIndex)
        {
            var passiveItem = passiveItemsSlots[slotIndex];
            if (!passiveItem.passiveItemData.NextLevelPrefab)
            {
                Debug.LogError("Next Level doesnt exist");
                return;
            }
            var upgradePassiveItem =
                Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            AddPassiveItem(slotIndex, upgradePassiveItem.GetComponent<PassiveItems>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[slotIndex] = upgradePassiveItem.GetComponent<PassiveItems>().passiveItemData.Level;
            if (levelUp != null && levelUp.choosingUpgrade)
            {
                levelUp.EndLevelUp(); 
            }
        }
    }

    private void AssignWeaponUpgrade(UpgradeUI upgradeOption)
    {
        WeaponUpgrade chosenWeaponUpgrade = weaponUpgradeOptions[Random.Range(0, weaponUpgradeOptions.Count)];
        if (chosenWeaponUpgrade != null)
        {
            int existingWeaponIndex = -1; 
            for (int i = 0; i < weaponSlots.Count; i++)
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
    }
    
    private void AssignPassiveItemUpgrade(UpgradeUI upgradeOption)
    {
        PassiveItemsUpgrade chosenPassiveItemsUpgrade = passiveUpgradeOptions[Random.Range(0, passiveUpgradeOptions.Count)];
        if (chosenPassiveItemsUpgrade != null)
        {
            int existingItemIndex = -1; 
            for (int i = 0; i < passiveItemsSlots.Count; i++)
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
    }
    
    private void ApplyUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            int upgradeType = Random.Range(1, 3);
            if (upgradeType == 1)
            {
                AssignWeaponUpgrade(upgradeOption);
            }
            else if (upgradeType == 2)
            {
                AssignPassiveItemUpgrade(upgradeOption);
            }
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
        upgradeOption.upgradeButton.onClick.RemoveAllListeners(); 
        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));
        upgradeOption.upgradeDescription.text = chosenWeaponUpgrade.weaponData.Description;
        upgradeOption.upgradeName.text = chosenWeaponUpgrade.weaponData.Name;
        upgradeOption.upgradeImage.sprite = chosenWeaponUpgrade.weaponData.Image;
    }

    private void UpgradeWeaponUI(UpgradeUI upgradeOption, WeaponUpgrade chosenWeaponUpgrade, int existingWeaponIndex)
    {
        upgradeOption.upgradeButton.onClick.RemoveAllListeners(); 
        upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(existingWeaponIndex));
        upgradeOption.upgradeDescription.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
        upgradeOption.upgradeName.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
        upgradeOption.upgradeImage.sprite = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Image;
    }

    private void NewPassiveItemUI(UpgradeUI upgradeOption, PassiveItemsUpgrade chosenPassiveItemsUpgrade)
    {
        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemsUpgrade.initialPassiveItem));
        upgradeOption.upgradeDescription.text = chosenPassiveItemsUpgrade.passiveItemData.Description;
        upgradeOption.upgradeName.text = chosenPassiveItemsUpgrade.passiveItemData.Name;
        upgradeOption.upgradeImage.sprite = chosenPassiveItemsUpgrade.passiveItemData.Image;
    }

    private void UpgradePassiveItemUI(UpgradeUI upgradeOption, PassiveItemsUpgrade chosenPassiveItemsUpgrade, int existingItemIndex)
    {
        upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(existingItemIndex));
        upgradeOption.upgradeDescription.text = chosenPassiveItemsUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItems>().passiveItemData.Description;
        upgradeOption.upgradeName.text = chosenPassiveItemsUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItems>().passiveItemData.Name;
        upgradeOption.upgradeImage.sprite = chosenPassiveItemsUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItems>().passiveItemData.Image;
    }
}
