using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : WeaponController
{
    [SerializeField] private float destroyDelay;
    private float currentDamage;
    private GameObject player;
    [SerializeField] private float radius = 2.0f;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private float spriteRotationSpeed = 360.0f;
    private float angle;
    private AudioManager audioManager;
    

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager.PlayGameSound(audioManager.spinAttack);
    }

    protected override void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        var x = player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        var y = player.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = new Vector2(x, y);
        
        transform.Rotate(0, 0, spriteRotationSpeed * Time.deltaTime);
        
        Destroy(gameObject, weaponData.CooldownDuration);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }

    protected override void Attack()
    {
        base.Attack();
        var spawnedSpinAttack = Instantiate(weaponData.Prefab);
        spawnedSpinAttack.transform.position = transform.position;
    }
}
