using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            //InventorySlot slot = eventData.pointerDrag.GetComponent<InventoryItem>();
            //InventorySlot.parentAfterDrag = transform;
        }
    }
}
