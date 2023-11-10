using UnityEngine;

public class SharkAttack : WeaponController
{
    protected override void Attack()
    {
        base.Attack();
        var spawnedSharkAttack = Instantiate(weaponData.Prefab);
        spawnedSharkAttack.transform.position = transform.position;
    }
}
