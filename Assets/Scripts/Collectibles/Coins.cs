using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Inventory.instance.AddCoins(50);
            Destroy(gameObject);
        }
    }
}
