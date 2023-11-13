using System;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
     private GameObject player;

     private void Update()
     {
         var pl = FindObjectOfType<Player>();
         if (pl != null) 
         {
             player = pl.gameObject;
         }
     }

    private void LateUpdate()
    {
        var position = player.transform.position;
        position.z = -10;
        transform.position = position;
    }
}
