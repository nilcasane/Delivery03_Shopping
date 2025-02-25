using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    // NOTE: Inventory UI slots support drag&drop,
    // implementing the Unity provided interfaces by events system

    public Image Image;
    public TextMeshProUGUI AmountText;
    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public RectTransform Description;
    public RectTransform SelectedCursor;

    public bool IsItemSelected;

    private Canvas _canvas;
    private GraphicRaycaster _raycaster;
    private Transform _parent;

    public ItemBase Item { get; private set;}
    public InventoryUI InventoryUI { get; private set;}

    public static Action<InventorySlotUI> OnItemSelected;

    [HideInInspector] public Transform parentAfterDrag;

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        _parent = GetComponent<RectTransform>();
        IsItemSelected = false;

        Item = slot.Item;
        InventoryUI = inventory;

        Image.sprite = Item.Image;

        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = slot.Amount > 1;

        ValueText.text = slot.Value.ToString();

        TitleText.text = Item.Name.ToString();
        DescriptionText.text = Item.Description.ToString();
    }

    private void OnEnable()
    {
        Player.OnResetSelectedItems += ResetCursor;
    }

    private void OnDisable()
    {
        Player.OnResetSelectedItems -= ResetCursor;
    }

    private void ResetCursor()
    {
        SelectedCursor.gameObject.SetActive(Player.SelectedItem == gameObject); 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Description.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Description.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Player.OnSelectedItem?.Invoke(gameObject);
        OnItemSelected?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parent = transform.parent;

        // Start moving object from the beginning!
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);

        // We need a few references from UI
        if (!_canvas)
        {
            _canvas = GetComponentInParent<Canvas>();
            _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        }

        // Change parent of our item to the canvas
        transform.SetParent(_canvas.transform, true);

        // And set it as last child to be rendered on top of UI
        transform.SetAsLastSibling();

        Image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Moving object around screen using mouse delta
        Vector3 adjustedDelta = new Vector3(eventData.delta.x / _canvas.scaleFactor, eventData.delta.y / _canvas.scaleFactor, 0);
        transform.localPosition += adjustedDelta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Use GraphicRaycaster for collision detections with the UI
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(eventData, results);

        InventoryUI targetInventoryUI = null;

        foreach (var result in results)
        {
            targetInventoryUI = result.gameObject.GetComponent<InventoryUI>();
            if (targetInventoryUI != null)
            {
                break;
            }
        }

        if (targetInventoryUI != null)
        {
            var targetInventory = targetInventoryUI.Inventory;

            if (InventoryUI.Inventory.Type == InventoryType.Player && targetInventory.Type == InventoryType.Shop)
            {
                // Buy item
                InventoryManager.Instance.SellItem(Item);
            }
            else if (InventoryUI.Inventory.Type == InventoryType.Shop && targetInventory.Type == InventoryType.Player)
            {
                // Sell item
                InventoryManager.Instance.BuyItem(Item);
            }
        }

        // Changing parent back to slot
        transform.SetParent(_parent.transform);

        // And centering item position
        transform.localPosition = Vector3.zero;

        Image.raycastTarget = true;
    }
}
