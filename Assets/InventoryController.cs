using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject SlotPrefabs;
    public int SlotCount;
    public GameObject[] ItemPrefabs;
    
    void Start()
    {
        for (int i = 0 ; i < SlotCount ; i++)
        {
            Slot slot = Instantiate(SlotPrefabs, InventoryPanel.transform).GetComponent<Slot>();
            if (i < ItemPrefabs.Length)
            {
                GameObject item = Instantiate(ItemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.CurrentItem = item;
            }
        }
    }

    
}
