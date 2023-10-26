using System;
using UnityEngine;

public class MagnetController : MonoBehaviour, ICollectible
{
      public float attractionStrength = 10f;
      private Transform playerTransform;

      
      private void Start()
      {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
      }

      public void Collect()
      {
            AttractCollectibles();
            Destroy(gameObject);
      }

      private void AttractCollectibles()
      {
            var collectibles = GameObject.FindGameObjectsWithTag("XpCollectible");

            foreach (var item in collectibles)
            {
                  var magnetItem = item.GetComponent<MagnetItem>();
                  if (magnetItem)
                  {
                        magnetItem.GetAttracted();
                  }
            }
      }
}
