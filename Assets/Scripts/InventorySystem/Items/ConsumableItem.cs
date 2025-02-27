using UnityEngine;

public abstract class ConsumableItem : ItemBase
{
    public int HealthPoints;
    public abstract void Use(IConsume consumer);
}
