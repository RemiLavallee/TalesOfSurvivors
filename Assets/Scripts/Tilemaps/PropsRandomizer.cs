using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PropsRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> propSpawnPoints;
    [SerializeField] private List<GameObject> propPrefabs;
    
    private void Start()
    {
        SpawnProps();
    }

    private void SpawnProps()
    {
        foreach (var sp in propSpawnPoints)
        {
            var rand = Random.Range(0, propPrefabs.Count);
            var prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
