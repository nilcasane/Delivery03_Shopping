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
    [SerializeField] public InventoryType Type;
    List<ItemSlot> Slots;
    private int maximumSlots = 18;
    
    public int Length => Slots.Count;

    public Action OnInventoryChange;

    public bool AddItem(ItemBase item)
    {
        if (Slots == null) Slots = new List<ItemSlot>();

        var slot = GetSlot(item);

        if ((slot!=null) && (item.IsStackeable))
        {
            slot.AddOne();
            OnInventoryChange?.Invoke();
            return true;
        }
        else if (Slots.Count != maximumSlots)
        {
            slot = new ItemSlot(item);
            Slots.Add(slot);
            OnInventoryChange?.Invoke();
            return true;
        }
        return false;
    }

    public bool RemoveItem(ItemBase item)
    {
        if (Slots != null)
        {
            var slot = GetSlot(item);

            if (slot != null)
            {
                slot.RemoveOne();
                if (slot.IsEmpty()) RemoveSlot(slot);
                OnInventoryChange?.Invoke();
                return true;
            }
        }
        return false;
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
}
