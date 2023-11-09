using UnityEngine;

public class SharkAttack : WeaponController
{
    [SerializeField] private Transform parentObject;
    
    protected override void Attack()
    {
        base.Attack();
        var spawnedSharkAttack = Instantiate(weaponData.Prefab, parentObject);
        spawnedSharkAttack.transform.position = transform.position;
    }
}
