using UnityEngine;
using UnityEngine.UI;

public class PlayerXpBar : MonoBehaviour
{
    public PlayerStats player;
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

        var fillValue = (float)player.experience / player.experienceCap;
        slider.value = fillValue;
    }
}
