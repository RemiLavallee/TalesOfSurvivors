using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    public SkillScriptableObject skillData;
    [SerializeField] private float destroyDelay;
    private float currentDamage;
    private GameObject player;
    [SerializeField] private float radius = 2.0f;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private float spriteRotationSpeed = 360.0f;
    private float angle;
    [SerializeField] private AudioSource spinAttackSound;

    private void Awake()
    {
        currentDamage = skillData.Damage ;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spinAttackSound.Play();
    }

    private void Update()
    {
        angle += rotationSpeed * Time.fixedDeltaTime;
        var x = player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        var y = player.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = new Vector2(x, y);
        
        transform.Rotate(0, 0, spriteRotationSpeed * Time.fixedDeltaTime);
        
        Destroy(gameObject, destroyDelay);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }
}