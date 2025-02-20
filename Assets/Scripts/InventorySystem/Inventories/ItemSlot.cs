using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    public ItemBase Item;
    public int Amount;
    public int Value;

    public ItemSlot(ItemBase item)
    {
        Item = item;
        Value = item.Value;
        Amount = 1;
    }

    internal bool HasItem(ItemBase item)
    {
        return (item == Item);
    }

    internal bool CanHold(ItemBase item)
    {
        if (item.IsStackeable) return (item == Item);
        return false;
    }

    internal void AddOne()
    {
        Amount++;
    }

    internal void RemoveOne()
    {
        Amount--;
    }

    public bool IsEmpty()
    {
        return Amount < 1;
    }
}
