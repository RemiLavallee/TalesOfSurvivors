using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<SkillScriptableObject> activeWeapons = new List<SkillScriptableObject>();
    public GameObject levelUpPanel;

    public void ChooseWeapon(SkillScriptableObject chosenWeapon)
    {
        activeWeapons.Add(chosenWeapon);
        levelUpPanel.SetActive(false);
    }

    public void LevelupUI()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0;
    }
    
}
