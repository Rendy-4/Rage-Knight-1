using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform OriginalParent;
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        OriginalParent = transform.parent; //Save OG Parent
        transform.SetParent(transform.root); // Above other Canvas
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; // Semi Transparency During Drag
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // Follow The Mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       canvasGroup.blocksRaycasts = true; // Enable Raycast 
       canvasGroup.alpha = 1f; //No longer Semi Transparent

       Slot dropslot = eventData.pointerEnter?.GetComponent<Slot>(); // Slot Where Item is Dropped
       Slot OriginalSlot = OriginalParent.GetComponent<Slot>();

        if (dropslot != null)
        {
            if(dropslot.CurrentItem == null)
            {
            dropslot.CurrentItem.transform.SetParent(OriginalParent.transform);
            OriginalSlot.CurrentItem = dropslot.CurrentItem;
            dropslot.CurrentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }

            else
            {
            OriginalSlot.CurrentItem = null;
            }

            //Move item Into Dropslot
            transform.SetParent(dropslot.transform);
            dropslot.CurrentItem = gameObject;
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Return To OG Position
        }
    }
}
