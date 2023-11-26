using System;
using UnityEngine;
using UnityEngine.UI;

public class ShaderBarXp : MonoBehaviour
{
    private Image image;
    private Material mat;
    private int shaderBool;

    private void Start()
    {
        image = GetComponent<Image>();
        mat = image.material;
        shaderBool = Shader.PropertyToID("_isActive");
    }

    private void Update()
    {
        mat.SetFloat("_UnscaledTime", Time.unscaledTime);
    }

    public void SetActiveBarShader()
    {
        image.color = Color.white;
        mat.SetInt(shaderBool, 1);
    }
    
    public void SetInactiveBarShader()
    {
        image.color = Color.cyan;
        mat.SetInt(shaderBool, 0);
    }
}
