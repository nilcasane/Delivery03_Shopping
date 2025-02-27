using UnityEngine;
using UnityEngine.UI;

public class BuyLogic : MonoBehaviour
{
    private Button _button;

    private void OnEnable()
    {
        Player.OnSelectedItem += CheckItemType;
    }

    private void OnDisable()
    {
        Player.OnSelectedItem -= CheckItemType;
    }

    void Start()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
        _button.onClick.AddListener(OnButtonClicked);
    }

    void CheckItemType(GameObject selectedItem)
    {
        if (selectedItem == null) _button.interactable = false;
        else
        {
            var InventorySlotUI = selectedItem.GetComponent<InventorySlotUI>();
            if (InventorySlotUI != null)
            {
                var InventoryUI = InventorySlotUI.InventoryUI;
                if (InventoryUI != null)
                {
                    var Inventory = InventoryUI.Inventory;
                    _button.interactable = (Inventory != null && Inventory.Type == InventoryType.Shop);
                }
            }
        }
    }

    void OnButtonClicked()
    {
        var selectedItem = Player.SelectedItem.GetComponent<InventorySlotUI>();
        if (selectedItem != null && _button.interactable)
        {
            InventoryManager.OnBuyItem?.Invoke(selectedItem.Item);
        }
    }
}
