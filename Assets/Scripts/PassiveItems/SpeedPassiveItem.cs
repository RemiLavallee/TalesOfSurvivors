
public class SpeedPassiveItem : PassiveItems
{
    protected override void ApplyModifier()
    {
        player.initialSpeed *=  1 + passiveItemData.Multipler / 100f;
    }
}
