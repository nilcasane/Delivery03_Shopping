using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Inventory System/Items/Food")]
public class ItemFood : ConsumableItem
{
    public int healthPoints;
    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}
