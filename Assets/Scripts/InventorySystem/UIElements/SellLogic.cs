using UnityEngine;
using UnityEngine.UI;

public class SellLogic : MonoBehaviour
{
    private InventoryManager inventoryManager;
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
        inventoryManager = InventoryManager.Instance;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    void CheckItemType(InventorySlotUI itemSlot)
    {
        var Inventory = itemSlot.InventoryUI.Inventory;
        _button.interactable = (Inventory.Type == InventoryType.Player);
    }
    void OnButtonClicked()
    {
        var selectedItem = Player.SelectedItem.GetComponent<InventorySlotUI>();
        if (selectedItem != null && _button.interactable)
        {
            inventoryManager.SellItem(selectedItem.Item);
        }
    }
}
