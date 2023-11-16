using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor;
    [SerializeField] private float flashTime;
    private SpriteRenderer sr;
    private Material mat;
    private Coroutine hitFlashCoroutine;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;
    }

    public void CallHitFlash()
    {
        StartCoroutine(HitFlash());
    }

    private IEnumerator HitFlash()
    {
        SetFlashColor();

        float currentFlashAmount = 0f;
        float elaspedTime = 0f;
        while (elaspedTime < flashTime)
        {
            elaspedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elaspedTime / flashTime));
            SetFlashAmount(currentFlashAmount);
            
            yield return null;
        }
    }

    private void SetFlashColor()
    {
        mat.SetColor("_FlashColor", flashColor);
    }

    private void SetFlashAmount(float amount)
    {
        mat.SetFloat("_FlashAmount", amount );
    }
}
