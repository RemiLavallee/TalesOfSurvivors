using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private AudioManager audioManager;
    
    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    } 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.gameObject.TryGetComponent(out ICollectible collectible))
          {
              collectible.Collect();
          }
          
          if (collision.CompareTag("XpCollectible"))
          {
              audioManager.PlayGameSound(audioManager.xp);
          }
    }
}
