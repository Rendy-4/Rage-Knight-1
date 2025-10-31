using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAbleItem : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler
{
    [HideInInspector] public Transform ParentAfterDrag;
    public Image image;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Memulai Memegang Item");
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Memegang Item");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Melepas Item");
        transform.SetParent(ParentAfterDrag);
        image.raycastTarget = true;
    } 
}
