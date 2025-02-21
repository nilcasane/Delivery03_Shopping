using System;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType
{
    Player,
    Shop
}

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField]
    List<ItemSlot> Slots;
    public int Length => Slots.Count;

    [SerializeField]
    public InventoryType Type;

    [SerializeField]
    int Hola;

    public Action OnInventoryChange;

    public void AddItem(ItemBase item)
    {
        if (Slots == null) Slots = new List<ItemSlot>();

        var slot = GetSlot(item);

        if ((slot!=null) && (item.IsStackeable))
        {
            slot.AddOne();
        }
        else
        {
            slot = new ItemSlot(item);
            Slots.Add(slot);
        }

        OnInventoryChange?.Invoke();
    }

    public void RemoveItem(ItemBase item)
    {
        if (Slots == null) return;

        var slot = GetSlot(item);

        if (slot != null)
        {
            slot.RemoveOne();
            if (slot.IsEmpty()) RemoveSlot(slot);
        }

        OnInventoryChange?.Invoke();
    }

    private void RemoveSlot(ItemSlot slot)
    {
        Slots.Remove(slot);
    }

    public void ClearInventory()
    {
        Slots.Clear();
    }

    private ItemSlot GetSlot(ItemBase item)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].HasItem(item)) return Slots[i];
        }
        return null;
    }

    public ItemSlot GetSlot(int i)
    {
        return Slots[i];
    }

    public void BuyItem(ItemBase item)
    {
        if (Player.Money >= item.Value)
        {
            Player.OnMoneyUpdated?.Invoke(-item.Value);
            RemoveItem(item);
        }
    }

    public void SellItem(ItemBase item)
    {

    }
}
