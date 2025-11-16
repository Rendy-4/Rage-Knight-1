using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;

        transform.SetParent(transform.root); // bawa ke atas UI
        canvasGroup.blocksRaycasts = false;  // biar slot bisa detect OnDrop
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Jika TIDAK kena slot
        if (transform.parent == transform.root)
        {
            // KEMBALIKAN ITEM
            transform.SetParent(originalParent);
            transform.localPosition = originalPosition;
        }
    }
}
