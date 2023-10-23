using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, ICollectible
{
    public float heal;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.Healing(heal);
        Destroy(gameObject);
    }

}
