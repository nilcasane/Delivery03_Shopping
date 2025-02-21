using UnityEngine;
using UnityEngine.UI;

public class SellLogic : MonoBehaviour
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
    void Start()
    {
        _button = GetComponent<Button>();
    }

    void CheckItemType(InventorySlotUI itemSlot)
    {
        var Inventory = itemSlot.InventoryUI.Inventory;
        _button.interactable = (Inventory.Type == InventoryType.Player);
    }
}
