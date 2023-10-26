using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    private bool isAttracted = false;
    private Transform playerTransform;
    public float attractionSpeed = 5f; 
    
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        if (isAttracted)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, attractionSpeed * Time.deltaTime);
        }
    }
    
    public void GetAttracted()
    {
        isAttracted = true;
    }
}
