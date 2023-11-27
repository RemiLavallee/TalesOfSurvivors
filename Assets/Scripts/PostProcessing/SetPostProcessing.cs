using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPostProcessing : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private readonly Color clickedColor = Color.white;
    private readonly Color originalColor = Color.gray;
    private bool isClicked = true;
    private PostProcessing postProcessingScript;
    
    public enum EffectType { Vignette, Chromatic }
    public EffectType effectType;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = clickedColor;
        postProcessingScript = FindObjectOfType<PostProcessing>();
    }

    public void OnMouseDown()
    {
        isClicked = !isClicked;
        spriteRenderer.color = isClicked ? clickedColor : originalColor;
        
        switch (effectType)
        {
            case EffectType.Vignette:
                postProcessingScript.ToggleVignette(isClicked);
                break;
            
            case EffectType.Chromatic:
                postProcessingScript.ToggleChromatic(isClicked);
                break;
        }
    }
}
