using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public AudioSource xp;
    
    private void OnTriggerEnter2D(Collider2D collision)
  {
      if (collision.gameObject.TryGetComponent(out ICollectible collectible))
      {
          collectible.Collect();
      }
      
      if (collision.CompareTag("XpCollectible"))
      {
          xp.Play();
      }
  }
}
