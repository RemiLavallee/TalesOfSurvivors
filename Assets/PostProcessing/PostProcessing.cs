using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{
  Volume volume; 
   Vignette vignette;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
    }

    private void Update()
    {
        vignette.intensity.Override(0.5f);
        vignette.color.Override(Color.black);
        vignette.smoothness.Override(0.2f);
    }
}
