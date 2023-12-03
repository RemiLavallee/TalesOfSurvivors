using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private float maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Color spriteColor;

    public GameObject Weapon { get => weapon; private set => weapon = value; }
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    public Color SpriteColor { get => spriteColor; private set => spriteColor = value; }
}
