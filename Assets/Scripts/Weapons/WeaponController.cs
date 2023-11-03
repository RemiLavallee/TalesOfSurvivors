using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public SkillScriptableObject weaponData;
    public float currentCooldown;
    

    protected virtual void Start()
    {
        Attack();
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
