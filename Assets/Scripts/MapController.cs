using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrainChunks;
    [SerializeField] private GameObject player;
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
            // samething (pm.movement.x > 0 && pm.movement.y == 0)
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
        if (Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask)) return;
        noTerrainPosition = currentChunk.transform.Find("Right").position;
        SpawnChunk();
    }

    private void ChunkCheckLeft()
    {
        if (Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask)) return;
        noTerrainPosition = currentChunk.transform.Find("Left").position;
        SpawnChunk();
    }

    private void ChunkCheckUp()
    {
        if (Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask)) return;
        noTerrainPosition = currentChunk.transform.Find("Up").position;
        SpawnChunk();
    }

    private void ChunkCheckDown()
    {
        if (Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask)) return;
        noTerrainPosition = currentChunk.transform.Find("Down").position;
        SpawnChunk();
    }

    private void ChunkCheckRightUp()
    {
        if (Physics2D.OverlapCircle(currentChunk.transform.Find("Right_Up").position, checkerRadius, terrainMask)) return;
        noTerrainPosition = currentChunk.transform.Find("Right_Up").position;
        SpawnChunk();
    }

    private void ChunkCheckRightDown()
    {
        if (Physics2D.OverlapCircle(currentChunk.transform.Find("Right_Down").position, checkerRadius, terrainMask)) return;
        noTerrainPosition = currentChunk.transform.Find("Right_Down").position;
        SpawnChunk();
    }

    private void ChunkCheckLeftUp()
    {
        if (Physics2D.OverlapCircle(currentChunk.transform.Find("Left_Up").position, checkerRadius, terrainMask)) return;
        noTerrainPosition = currentChunk.transform.Find("Left_Up").position;
        SpawnChunk();
    }

    private void ChunkCheckLeftDown()
    {
        if (Physics2D.OverlapCircle(currentChunk.transform.Find("Left_Down").position, checkerRadius, terrainMask)) return;
        noTerrainPosition = currentChunk.transform.Find("Left_Down").position;
        SpawnChunk();
    }

    private void SpawnChunk()
    {
        var rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
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
