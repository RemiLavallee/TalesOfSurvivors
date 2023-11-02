using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeEffect : MonoBehaviour
{
    public Image explodePanel;
    public float flashDuration = 1.0f;
    private bool isFlashing = false;
    private float flashTime = 0;

    private void Update()
    {
        if (isFlashing)
        {
            FlashEffect();
        }
    }

    public void StartFlash()
    {
        isFlashing = true;
        flashTime = 0;
    }

    private void FlashEffect()
    {
        flashTime += Time.deltaTime;
        var alpha = Mathf.Sin(flashTime / flashDuration * Mathf.PI);
        explodePanel.color = new Color(1f, 1f, 1f, alpha);

        if (flashTime >= flashDuration)
        {
            isFlashing = false;
            explodePanel.color = new Color(1f, 1f, 1f, 0f); 
        }
    }
}
