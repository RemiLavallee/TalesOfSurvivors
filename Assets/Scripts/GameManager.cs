using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
     public Sprite spriteToConvert; 
     [HideInInspector]
     public Texture2D convertedTexture; 

     private void Start()
     {
     
          convertedTexture = ConvertSpriteToTexture2D(spriteToConvert);
          Time.timeScale = 1f;
     
          Cursor.SetCursor(convertedTexture, Vector2.zero, CursorMode.Auto);
     }

     private Texture2D ConvertSpriteToTexture2D(Sprite sprite)
     {
          if (sprite.rect.width != sprite.texture.width)
          {
               Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
               newText.wrapMode = TextureWrapMode.Clamp;
               newText.filterMode = FilterMode.Bilinear;
               Color[] newColors = sprite.texture.GetPixels((int)sprite.rect.x, 
                    (int)sprite.rect.y, 
                    (int)sprite.rect.width, 
                    (int)sprite.rect.height);
               newText.SetPixels(newColors);
               newText.Apply();
               return newText;
          }
          else
               return sprite.texture;
     }
}
