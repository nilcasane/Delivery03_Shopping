using UnityEngine;
using UnityEngine.UI;

public class UseLogic : MonoBehaviour
{
    private Button _button;

    private void OnEnable()
    {
        InventorySlotUI.OnItemSelected += CheckItemType;
    }
    private void OnDisable()
    {
        InventorySlotUI.OnItemSelected -= CheckItemType;
    }
    private void Start()
    {
        _button = GetComponent<Button>();
    }
    private void CheckItemType(InventorySlotUI itemSlot)
    {
        var Item = itemSlot.Item;
        var Inventory = itemSlot.InventoryUI.Inventory;

        _button.interactable = (Inventory.Type == InventoryType.Player && Item is ConsumableItem);
    }
}
