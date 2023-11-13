using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrainChunks; 
    private GameObject player;
    [SerializeField] private float checkerRadius;
    private Vector3 noTerrainPosition;
    [SerializeField] private LayerMask terrainMask;
    private Player pm;
    [HideInInspector]public GameObject currentChunk;
    [Header("Optimization")] public List<GameObject> spawnedChunks;
    private GameObject latestChunk;
    [SerializeField] private float maxOpDistance;
    [SerializeField] private float opDistance;
    private float optimizerCooldown;
    [SerializeField] private float optimizerCdTime;

    private void Start()
    {
        var playerController = FindObjectOfType<Player>();
        if (playerController != null) 
        {
            player = playerController.gameObject;
        }
        
        
        pm = FindObjectOfType<Player>();
    }

    private void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    private void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        switch (pm.movement)
        {
            case { x: > 0, y: 0 }:
                ChunkCheckRight();
                break;
            case { x: < 0, y: 0 }:
                ChunkCheckLeft();
                break;
            case { x: 0, y: > 0 }:
                ChunkCheckUp();
                break;
            case { x: 0, y: < 0 }:
                ChunkCheckDown();
                break;
            case { x: > 0, y: > 0 }:
                ChunkCheckRightUp();
                break;
            case { x: < 0, y: > 0 }:
                ChunkCheckLeftUp();
                break;
            case { x: < 0, y: < 0 }:
                ChunkCheckLeftDown();
                break;
            case { x: > 0, y: < 0 }:
                ChunkCheckRightDown();
                break;
        }
    }

    private void ChunkCheckRight()
    {
        var rightTransform = currentChunk.transform.Find("Right");
        if (rightTransform == null || Physics2D.OverlapCircle(rightTransform.position, checkerRadius, terrainMask))
            return;
        noTerrainPosition = rightTransform.position;
        SpawnChunk();
    }

    private void ChunkCheckLeft()
    {
        var leftTransform = currentChunk.transform.Find("Left");
        if (leftTransform == null || Physics2D.OverlapCircle(leftTransform.position, checkerRadius, terrainMask))
            return;
        noTerrainPosition = leftTransform.position;
        SpawnChunk();
    }

    private void ChunkCheckUp()
    {
        var upTransform = currentChunk.transform.Find("Up");
        if (upTransform == null || Physics2D.OverlapCircle(upTransform.position, checkerRadius, terrainMask))
            return;
        noTerrainPosition = upTransform.position;
        SpawnChunk();
    }

    private void ChunkCheckDown()
    {
        var downTransform = currentChunk.transform.Find("Down");
        if (downTransform == null || Physics2D.OverlapCircle(downTransform.position, checkerRadius, terrainMask))
            return;
        noTerrainPosition = downTransform.position;
        SpawnChunk();
    }

    private void ChunkCheckRightUp()
    {
        var rightUpTransform = currentChunk.transform.Find("Right_Up");
        if (rightUpTransform == null || Physics2D.OverlapCircle(rightUpTransform.position, checkerRadius, terrainMask))
            return;
        noTerrainPosition = rightUpTransform.position;
        SpawnChunk();
    }

    private void ChunkCheckRightDown()
    {
        var rightDownTransform = currentChunk.transform.Find("Right_Down");
        if (rightDownTransform== null || Physics2D.OverlapCircle(rightDownTransform.position, checkerRadius, terrainMask))
            return;
        noTerrainPosition = rightDownTransform.position;
        SpawnChunk();
    }

    private void ChunkCheckLeftUp()
    {
        var leftUpTransform = currentChunk.transform.Find("Left_Up");
        if (leftUpTransform == null || Physics2D.OverlapCircle(leftUpTransform.position, checkerRadius, terrainMask))
            return;
        noTerrainPosition = leftUpTransform.position;
        SpawnChunk();
    }

    private void ChunkCheckLeftDown()
    {
        var leftDownTransform = currentChunk.transform.Find("Left_Down");
        if (leftDownTransform == null || Physics2D.OverlapCircle(leftDownTransform.position, checkerRadius, terrainMask))
            return;
        noTerrainPosition = leftDownTransform.position;
        SpawnChunk();
    }

    private void SpawnChunk()
    {
        var rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        latestChunk.transform.SetParent(transform);
        spawnedChunks.Add(latestChunk);
    }

    private void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCdTime;
        }
        else
        {
            return;
        }
        
        foreach (var chunk in spawnedChunks)
        {
            opDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            chunk.SetActive(opDistance <= maxOpDistance);
        }
    }
}
