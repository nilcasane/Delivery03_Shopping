using System;
using UnityEngine;

public enum PotionType
{
    HealthRestorer,
    HealthRemover
}

[CreateAssetMenu(fileName = "Potion", menuName = "Inventory System/Items/Potion")]
public class ItemPotion : ConsumableItem
{
    public PotionType Type;
    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}
