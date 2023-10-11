using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject mainAttackPrefab;
    public Player player;
    public GameObject healthBar;
    private Animator animator;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    [Header("Experience")] 
    public int experience = 0;
    public int level = 1;
    public int experienceCap;
    public int attack;
    public int speed;
    private int def;
    private int health;
    private int magnet;
    [SerializeField] private AudioSource attackSound;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    private void Awake()
    {
        currentHealth = maxHealth + health;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        experienceCap = levelRanges[0].experienceCapIncrease;
        InvokeRepeating("MainAttack", 1f, 1f);
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            var experienceCapIncrease = 0;
            foreach (var range in levelRanges)
            {
                if (level < range.startLevel || level > range.endLevel) continue;
                experienceCapIncrease = range.experienceCapIncrease;
                break;
            }
            experienceCap += experienceCapIncrease;
        }
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        animator.SetBool("isDead", true);
        SetInactive();
        Invoke("Stop", 0.5f);
    }

    public void Healing(float amount)
    {
        if (currentHealth <= maxHealth)
        {
            currentHealth += amount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    private void MainAttack()
    {
        attackSound.Play();
        var mainAttack = Instantiate(mainAttackPrefab, transform.position, Quaternion.identity);
        mainAttack.transform.right = player.directionToMouse;
        
    }
    
    public void Stop()
    {
        Time.timeScale = 0;
    }

    public void SetInactive()
    {
        healthBar.SetActive(false);
        player.enabled = false;
        CancelInvoke("MainAttack");
        player.GetComponent<Player>().enabled = false;
        player.rb.velocity = new Vector3(0,0,0);
        var playerCollider = player.GetComponent<BoxCollider2D>();
        playerCollider.isTrigger = true;
    }
}
