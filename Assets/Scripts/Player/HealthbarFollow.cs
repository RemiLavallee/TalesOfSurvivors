using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarFollow : MonoBehaviour
{
    private Transform player;
    public Vector3 offset = new Vector3(0, 2, 0);

    private void Awake()
    {
        var pl = FindObjectOfType<Player>();
        if (pl != null)
        {
            player = pl.transform;
        }
    }

    void Update()
    {
        transform.position = player.position + offset;
    }
}
