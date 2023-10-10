using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private float currentHealth;
    
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    public float Damage { get => damage; private set => damage = value; }
    public float CurrentHealth { get => currentHealth; private set => currentHealth = value; }
}
