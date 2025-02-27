using UnityEngine;
using UnityEngine.UI;

public class UseLogic : MonoBehaviour
{
    private Button _button;
    private ConsumableItem consumableItem;

    private void OnEnable()
    {
        Player.OnSelectedItem += CheckItemType;
    }
    private void OnDisable()
    {
        Player.OnSelectedItem -= CheckItemType;
    }
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
        _button.onClick.AddListener(OnButtonClicked);
    }
    private void CheckItemType(GameObject selectedItem)
    {
        if (selectedItem == null) _button.interactable = false;
        else
        {
            var InventorySlotUI = selectedItem.GetComponent<InventorySlotUI>();
            if (InventorySlotUI != null)
            {
                var InventoryUI = InventorySlotUI.InventoryUI;
                consumableItem = InventorySlotUI.Item as ConsumableItem;
                if (InventoryUI != null)
                {
                    var Inventory = InventoryUI.Inventory;
                    _button.interactable = (Inventory.Type == InventoryType.Player && consumableItem != null);
                }
            }
        }
    }
    private void OnButtonClicked()
    {
        if (consumableItem != null)
        {
            InventoryManager.OnUseItem?.Invoke(consumableItem);
        }
    }
}
