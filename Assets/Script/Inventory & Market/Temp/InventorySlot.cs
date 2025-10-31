using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Debug.Log("Item Telah DiLetakkan");
            GameObject Dropped = eventData.pointerDrag;
            DragAbleItem dragableitem = Dropped.GetComponent<DragAbleItem>();
            dragableitem.ParentAfterDrag = transform;
        }
        else
        {
            Debug.Log("Slot Sudah Terisi");
        }
       
    }
}
