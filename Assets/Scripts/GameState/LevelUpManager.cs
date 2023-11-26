using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager instance;
    
    public GameObject levelUpUI;
    public GameObject player;
    public bool choosingUpgrade;
    [SerializeField] private ParticleSystem[] particles = new ParticleSystem[3];
    private ShaderBarXp shaderBar;
    private PlayerStats levelUp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        shaderBar = FindObjectOfType<ShaderBarXp>();
        levelUp = FindObjectOfType<PlayerStats>();
    }

    public void LevelUp()
    {

        player.SendMessage("RemoveApplyUpgrades");
        
        if (InventoryManager.instance.AllPanelsDisabled())
        {
            EndLevelUp();
        }
        else
        {
            StartParticles();
            choosingUpgrade = true;
            levelUpUI.SetActive(true);
            shaderBar.SetActiveBarShader();
            Time.timeScale = 0f;
        }
    }

    public void EndLevelUp()
    {
        StopParticles();
        choosingUpgrade = false;
        levelUpUI.SetActive(false);
        Time.timeScale = 1f;
        shaderBar.SetInactiveBarShader();
        levelUp.EndLevelChecker();
    }
    
    private void StartParticles()
    {
        foreach (var particle in particles)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }
    }

    private void StopParticles()
    {
        foreach (var particle in particles)
        {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }
    }
}
