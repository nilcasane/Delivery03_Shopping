using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        //DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        //draggableItem.parentAfterDrag = transform;
    }
}
