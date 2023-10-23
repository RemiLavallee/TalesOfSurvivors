using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : MonoBehaviour, ICollectible
{
    public int expGive;
    public void Collect()
    {
        var player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(expGive);
        Destroy(gameObject);
    }
}
