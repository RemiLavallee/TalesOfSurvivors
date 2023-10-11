using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ICollectible
{
    [SerializeField] private AudioSource bombSound;
    public float explosionRadius = 10f;
    public LayerMask enemyLayer;
    
    public void Collect()
    {
        Explode();
        bombSound.Play();
        Destroy(gameObject);
    }

    private void Explode()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var enemiesToDestroy = new List<GameObject>();

        foreach (var enemy in enemies)
        {
            enemiesToDestroy.Add(enemy);
        }
        foreach (var enemy in enemiesToDestroy)
        {
            Destroy(enemy);
        }
    }
}
