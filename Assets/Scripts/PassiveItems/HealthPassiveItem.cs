
public class HealthPassiveItem : PassiveItems
{
    protected override void ApplyModifier()
    {
        player.maxHealth *= 1 + passiveItemData.Multipler / 100f;
    }
}
