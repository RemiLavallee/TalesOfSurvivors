using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        
    }

    public void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }
}
