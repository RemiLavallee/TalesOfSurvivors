using UnityEngine;

[CreateAssetMenu(fileName = "weaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class SkillScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private int level;
    [SerializeField] private GameObject nextLevelPrefab;
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    
    
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    public float Damage { get => damage; private set => damage = value; }
    public float Speed { get => speed; private set => speed = value; }
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    public int Level { get => level; private set => level = value; }
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
    public string Name { get => name; private set => name = value; }
    public string Description { get => description; private set => description = value; }
    public Sprite Image { get => image; private set => image = value; }
}