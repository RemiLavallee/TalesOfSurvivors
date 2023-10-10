using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatutBar : MonoBehaviour
{
    public EnemyStats enemyStats;
    public Image fillImage;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        
        var fillValue = enemyStats.currentHealth / enemyStats.currentMaxHealth;
        slider.value = fillValue;
    }
}