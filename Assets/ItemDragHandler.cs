using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalparent;
    CanvasGroup canvasGroup;
    
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalparent = transform.parent;

        transform.SetParent(transform.root);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slot dropslot = eventData.pointerEnter?.GetComponentInParent<Slot>();
        Slot originalslot = originalparent.GetComponent<Slot>();

        // Jika original slot tidak valid → kembalikan item
        if (originalslot == null)
        {
            ResetToOriginalSlot();
            return;
        }

        // Jika drop di slot lain
        if (dropslot != null)
        {
            // Jika slot baru ada item → swap
            if (dropslot.CurrentItem != null)
            {
                GameObject itemInDropSlot = dropslot.CurrentItem;

                // Pindahkan item lama ke slot asal
                itemInDropSlot.transform.SetParent(originalslot.transform);
                itemInDropSlot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                // Update data
                originalslot.CurrentItem = itemInDropSlot;
            }
            else
            {
                // Jika slot kosong
                originalslot.CurrentItem = null;
            }

            // Pindahkan item ini ke slot baru
            transform.SetParent(dropslot.transform);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Update data slot baru
            dropslot.CurrentItem = gameObject;

            return;
        }

        // Jika bukan slot → kembali ke slot asal
        ResetToOriginalSlot();
    }

    void ResetToOriginalSlot()
    {
        transform.SetParent(originalparent);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        Slot originalslot = originalparent.GetComponent<Slot>();
        if (originalslot != null)
        {
            originalslot.CurrentItem = gameObject;
        }
    }
}
