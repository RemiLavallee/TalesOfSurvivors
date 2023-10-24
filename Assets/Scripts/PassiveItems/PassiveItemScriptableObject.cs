using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/PassiveItems")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField] private float multipler;
    [SerializeField] private int level;
    [SerializeField] private GameObject nextLevelPrefab;
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    
    public float Multipler => multipler;
    public int Level { get => level; private set => level = value; }
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
    public string Name { get => name; private set => name = value; }
    public string Description { get => description; private set => description = value; }
    public Sprite Image { get => image; private set => image = value; }
}
