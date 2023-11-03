using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCursor : MonoBehaviour
{
    public Sprite spriteToConvert;
    [HideInInspector] public Texture2D convertedTexture;
    public List<Sprite> cursorAnimation;
    private bool isAnimating = false;
    public float animationSpeed = 0.1f;
    
    
    private void Start()
    {
        convertedTexture = ConvertSpriteToTexture2D(spriteToConvert);
        Time.timeScale = 1f;
        Cursor.SetCursor(convertedTexture, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isAnimating)
        {
            StartCoroutine(AnimateCursor());
        }
    }

    private IEnumerator AnimateCursor()
    {
        isAnimating = true;
        foreach (var sprite in cursorAnimation)
        {
            Texture2D texture = ConvertSpriteToTexture2D(sprite);
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
            yield return new WaitForSeconds(animationSpeed);
        }
        Cursor.SetCursor(convertedTexture, Vector2.zero, CursorMode.Auto);
        isAnimating = false;
    }

    
    private static Texture2D ConvertSpriteToTexture2D(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            var newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
            newText.wrapMode = TextureWrapMode.Clamp;
            newText.filterMode = FilterMode.Bilinear;
            var newColors = sprite.texture.GetPixels((int)sprite.rect.x,
                (int)sprite.rect.y,
                (int)sprite.rect.width,
                (int)sprite.rect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else return sprite.texture;
    }
}
