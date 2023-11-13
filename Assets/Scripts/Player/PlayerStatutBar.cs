using UnityEngine;
using UnityEngine.UI;

public class PlayerStatutBar : MonoBehaviour
{
     public PlayerStats playerHealth;
     public Image fillImage;
     private Slider slider;

     private void Awake()
     {
          playerHealth = FindObjectOfType<PlayerStats>();
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
          
          var fillValue = playerHealth.currentHealth / playerHealth.maxHealth;

          if (fillValue <= slider.maxValue / 3)
          {
               fillImage.color = Color.red;
          }
          else
          {
               fillImage.color = Color.green;
          }
          slider.value = fillValue;
     }
}