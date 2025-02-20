using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    // NOTE: Inventory UI slots support drag&drop,
    // implementing the Unity provided interfaces by events system

    public Image Image;
    public TextMeshProUGUI AmountText;
    public TextMeshProUGUI ValueText;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public RectTransform Description;

    private Canvas _canvas;
    private GraphicRaycaster _raycaster;
    private Transform _parent;
    private ItemBase _item;
    private InventoryUI _inventory;

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        _item = slot.Item;
        _inventory = inventory;

        Image.sprite = _item.Image;

        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = slot.Amount > 1;

        ValueText.text = slot.Value.ToString();

        TitleText.text = _item.Name.ToString();
        DescriptionText.text = _item.Description.ToString();
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
        Debug.Log("Item dragging: "+_item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Moving object around screen using mouse delta
        Vector3 adjustedDelta = new Vector3(eventData.delta.x / _canvas.scaleFactor, eventData.delta.y / _canvas.scaleFactor, 0);
        transform.localPosition += adjustedDelta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Find scene objects colliding with mouse point on end dragging
        RaycastHit2D hitData = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        var hitComp = hitData.collider?.GetComponent<InventoryUI>().Inventory;
        if (hitComp != null)
        {
            hitComp.AddItem(_item);
        }
        if (hitData)
        {
            Debug.Log("Drop over object: " + hitData.collider.gameObject.name);

            var consumer = hitData.collider.GetComponent<IConsume>();
            bool consumable = _item is ConsumableItem;

            if ((consumer != null) && consumable)
            {
                (_item as ConsumableItem).Use(consumer);
                _inventory.UseItem(_item);
            }
        }

        // Changing parent back to slot
        transform.SetParent(_parent.transform);

        // And centering item position
        transform.localPosition = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Description.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Description.gameObject.SetActive(false);
    }
}
