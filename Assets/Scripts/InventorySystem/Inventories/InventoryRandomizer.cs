using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryRandomizer : MonoBehaviour
{
    public Inventory Inventory;
    public ItemBase[] Items;
    public int numberItems;
    public ItemBase actualItem;

    private void OnEnable()
    {
        RerollLogic.OnInventoryReroll += Reroll;
    }

    private void OnDisable()
    {
        RerollLogic.OnInventoryReroll -= Reroll;
    }

    private void Reroll()
    {
        Debug.Log("Rerolling!!");
        if (Inventory == null || Items == null) return;

        Inventory.ClearInventory();

        numberItems = Random.Range(18, 24);

        for(int i = 0; i < numberItems; i++)
        {
            actualItem = Items[Random.Range(0, Items.Length)];
            actualItem.Value = Random.Range(actualItem.MinPrice, actualItem.MaxPrice);
            Inventory.AddItem(actualItem);
        }
    }
}
