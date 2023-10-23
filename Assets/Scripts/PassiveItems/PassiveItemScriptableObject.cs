using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/PassiveItems")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField] private float multipler;
    [SerializeField] private int level;
    [SerializeField] private GameObject nextLevelPrefab;
    public float Multipler => multipler;
    public int Level { get => level; private set => level = value; }
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
}
