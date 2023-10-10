using UnityEngine;

[CreateAssetMenu(fileName = "skillEnemyScriptableObject", menuName = "ScriptableObjects/EnemySkill")]
public class EnemySkillScriptable : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float destroyDelay;
    
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    public float Damage { get => damage; private set => damage = value; }
    public float Speed { get => speed; private set => speed = value; }
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    public float DistanceToPlayer { get => distanceToPlayer; private set => distanceToPlayer = value; }
    public float DestroyDelay { get => destroyDelay; private set => destroyDelay = value; }
}
