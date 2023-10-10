using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        var enemy = FindObjectOfType<Enemy>();
        enemy.Explode();
        Destroy(gameObject);
    }
}
