using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public GameObject currentItem;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject == null)
            return;

        ItemUI draggedUI = draggedObject.GetComponent<ItemUI>();
        ItemDragHandler dragHandler = draggedObject.GetComponent<ItemDragHandler>();
        Slot originSlot = dragHandler.originalParent.GetComponent<Slot>();

        // <<< CASE 1 : MERGE ITEM >>>
        if (currentItem != null)
        {
            ItemUI currentUI = currentItem.GetComponent<ItemUI>();

            if (currentUI.itemData == draggedUI.itemData)
            {
                currentUI.AddAmount(draggedUI.amount);
                originSlot.currentItem = null;
                Destroy(draggedObject);
                return;
            }
        }

        // <<< CASE 2 : SLOT KOSONG — PLACE ITEM >>>
        if (currentItem == null)
        {
            currentItem = draggedObject;
            originSlot.currentItem = null;

            draggedObject.transform.SetParent(transform);
            draggedObject.transform.localPosition = Vector3.zero;
            return;
        }

        // <<< CASE 3 : SWAP >>>
        GameObject targetItem = currentItem;

        // pindah dragged ke slot ini
        currentItem = draggedObject;
        draggedObject.transform.SetParent(transform);
        draggedObject.transform.localPosition = Vector3.zero;

        // pindah item lama ke slot asal
        originSlot.currentItem = targetItem;
        targetItem.transform.SetParent(originSlot.transform);
        targetItem.transform.localPosition = Vector3.zero;
    }
}
