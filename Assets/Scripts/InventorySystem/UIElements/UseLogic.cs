using UnityEngine;
using UnityEngine.UI;

public class UseLogic : MonoBehaviour
{
    private Button _button;
    private ConsumableItem consumableItem;
    private ConsumeItem _consumeItem;

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
        _consumeItem = GetComponent<ConsumeItem>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }
    private void CheckItemType(InventorySlotUI itemSlot)
    {
        consumableItem = itemSlot.Item as ConsumableItem;
        _button.interactable = itemSlot.InventoryUI.Inventory.Type == InventoryType.Player && consumableItem != null;
    }
    private void OnButtonClicked()
    {
        if (consumableItem != null)
        {
            InventoryManager.Instance.Use(consumableItem);
        }
    }
}
