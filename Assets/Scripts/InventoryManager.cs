using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public List<WeaponController> weaponSlots = new List<WeaponController>();
    public List<PassiveItems> passiveItemsSlots = new List<PassiveItems>();
    public int[] weaponLevels = new int[5];
    public int[] passiveItemLevels = new int[5];

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        //weaponLevels[slotIndex] = weapon
    }
    
    public void AddPassiveItem(int slotIndex, PassiveItems passiveItems)
    {
        passiveItemsSlots[slotIndex] = passiveItems;
        passiveItemLevels[slotIndex] = passiveItems.passiveItemData.Level;
    }

    public void LevelUpWeapon(int slotIndex)
    {
        
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        
    }
}
