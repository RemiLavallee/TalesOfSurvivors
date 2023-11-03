using System;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
     [Serializable]
     public class Drops
     {
          public string name;
          public GameObject itemPrefab;
          public float dropRate;
     }

     public List<Drops> drops;

     public void OnDestroy()
     {
         if (!gameObject.scene.isLoaded)
         {
             return;
         }
         
         var randomNumber = UnityEngine.Random.Range(0f, 100f);
         var possibleDrops = new List<Drops>();
         
         foreach (var rate in drops)
         {
             if (randomNumber <= rate.dropRate)
             {
                 possibleDrops.Add(rate);
             }
         }

         if (possibleDrops.Count > 0)
         {
             var drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
             Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
         }
     }
     
    // private void DropItem()
}
