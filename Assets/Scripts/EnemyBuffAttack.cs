using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffAttack : MonoBehaviour
{
    public EnemySkillScriptable skillData;
    private float currentDamage;
    
    
    private void Awake()
    {
        currentDamage = skillData.Damage;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    
    private void Update()
    {
        transform.position += transform.right * (skillData.Speed * Time.deltaTime);
        Destroy(gameObject, skillData.DestroyDelay);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
    
}
