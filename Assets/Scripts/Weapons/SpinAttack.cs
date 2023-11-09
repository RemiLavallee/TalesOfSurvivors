using UnityEngine;

public class SpinAttack : WeaponController
{
    private AudioManager audioManager;
    [SerializeField] private Transform parentObject;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected override void Start()
    {
        base.Start();
        audioManager.PlayGameSound(audioManager.spinAttack);
    }

    protected override void Attack()
    {
        base.Attack();
        var spawnedSpinAttack = Instantiate(weaponData.Prefab, parentObject);
        spawnedSpinAttack.transform.position = transform.position;
    }
}
