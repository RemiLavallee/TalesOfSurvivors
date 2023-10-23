using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 2, 0);
    void Update()
    {
        transform.position = player.position + offset;
    }
}
