using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{
    private Volume volume; 
    private Vignette vignette;
    private ChromaticAberration chromatic;
    private PlayerStats player;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out chromatic);
        player = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        if (player.initialSpeed > 200)
        {
            chromatic.intensity.Override(0.2f);
        }
    }
    
    public void ToggleVignette(bool active)
    {
        vignette.active = active;
        vignette.intensity.Override(0.5f);
        vignette.smoothness.Override(1f);
    }
    
    public void ToggleChromatic(bool active)
    {
        chromatic.active = active;
        chromatic.intensity.Override(0f);
    }
}