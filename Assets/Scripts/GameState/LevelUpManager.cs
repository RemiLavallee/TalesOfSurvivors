using System;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager instance;
    
    public GameObject levelUpUI;
    public GameObject player;
    public bool choosingUpgrade;
    [SerializeField] private ParticleSystem[] particles = new ParticleSystem[3];

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

    public void LevelUp()
    {
        StartParticles();
        choosingUpgrade = true;
        levelUpUI.SetActive(true);
        Time.timeScale = 0f;
        player.SendMessage("RemoveApplyUpgrades");
    }

    public void EndLevelUp()
    {
        StopParticles();
        choosingUpgrade = false;
        levelUpUI.SetActive(false);
        Time.timeScale = 1f;
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
