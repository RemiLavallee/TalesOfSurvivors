using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //[SerializeField] private GameObject mainAttackPrefab;
    public Player player;
    public GameObject healthBar;
    private Animator animator;
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private CharacterScriptableObject characterData;
    //[SerializeField] private CharachterStat stats;

    [Header("Leveling")] public int experience = 0;
    public int level = 1;
    public int experienceCap;
    public float initialSpeed;
    public TextMeshProUGUI xpText;
    private AudioManager audioManager;
    [SerializeField] private TextMeshProUGUI levelCount;
    private GameObject playerColor;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    private InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    public void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();
        inventory = GetComponent<InventoryManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        experienceCap = levelRanges[0].experienceCapIncrease;
        InvokeRepeating("MainAttack", 1f, 1f);
        if (player != null && characterData != null)
        {
            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = characterData.SpriteColor;
            }
        }
        
        if (CharachterStat.instance != null)
        {
            maxHealth = CharachterStat.instance.health + characterData.MaxHealth;
            currentHealth = maxHealth;
            initialSpeed = CharachterStat.instance.speed + characterData.MoveSpeed;
        }
        
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    private void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            audioManager.PlayGameSound(audioManager.levelUp);
            level++;
            LevelUpManager.instance.LevelUp();
        }
        
        xpText.text = $"{experience}/{experienceCap}";
    }

    public void EndLevelChecker()
    {
        experience -= experienceCap;
        levelCount.text = level.ToString();

        var experienceCapIncrease = 0;
        foreach (var range in levelRanges)
        {
            if (level < range.startLevel || level > range.endLevel) continue;
            experienceCapIncrease = range.experienceCapIncrease;
            break;
        }
        
        experienceCap += experienceCapIncrease;
        xpText.text = $"{experience}/{experienceCap}";
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

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
        audioManager.PlayGameSound(audioManager.mainAttack);
        var mainAttack = Instantiate(characterData.Weapon, transform.position, Quaternion.identity);
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
        player.rb.velocity = new Vector3(0, 0, 0);
        var playerCollider = player.GetComponent<BoxCollider2D>();
        playerCollider.isTrigger = true;
    }

    public void SpawnWeapon(GameObject weapon)
    {
        var spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());
        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        var spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItems>());
        passiveItemIndex++;
    }
}