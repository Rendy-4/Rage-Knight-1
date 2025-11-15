using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject SlotPrefabs;
    public int SlotCount;
    public ItemData[] startingItems;
    public GameObject[] ItemPrefabs;

    private List<Slot> slots = new List<Slot>();

    void Start()
    {
        GenerateSlots();

        // Spawn item awal (opsional)
        for (int i = 0; i < startingItems.Length && i < slots.Count; i++)
        {
            SpawnItemIntoSlot(startingItems[i], slots[i]);
        }
    }

    // ------------------------------ //
    //          GENERATE SLOT         //
    // ------------------------------ //
    private void GenerateSlots()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            Slot slot = Instantiate(SlotPrefabs, InventoryPanel.transform).GetComponent<Slot>();
            slots.Add(slot);
        }
    }

    // ------------------------------ //
    //       SPAWN ITEM KE SLOT       //
    // ------------------------------ //
    private void SpawnItemIntoSlot(ItemData data, Slot slot)
    {
        GameObject prefab = FindPrefabForItem(data);
        if (prefab == null) return;

        GameObject item = Instantiate(prefab, slot.transform);
        item.GetComponent<ItemUI>().Setup(data);
        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        slot.CurrentItem = item;
    }

    // ------------------------------ //
    //     CLEAR INVENTORY (SAVE)     //
    // ------------------------------ //
    public void ClearInventory()
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
                slot.CurrentItem = null;
            }
        }
    }

    // ------------------------------ //
    //     LOAD INVENTORY (LOAD)      //
    // ------------------------------ //
    public void LoadInventory(List<string> itemIDs)
    {
        for (int i = 0; i < itemIDs.Count && i < slots.Count; i++)
        {
            if (string.IsNullOrEmpty(itemIDs[i]))
                continue; // slot kosong

            ItemData data = ItemDatabase.Instance.GetItemByID(itemIDs[i]);
            if (data == null)
            {
                Debug.LogWarning("Item ID tidak ditemukan: " + itemIDs[i]);
                continue;
            }

            SpawnItemIntoSlot(data, slots[i]);
        }
    }

    // ------------------------------ //
    //     GET ALL ITEM IDs (SAVE)    //
    // ------------------------------ //
    public List<string> GetAllItemIDs()
    {
        List<string> ids = new List<string>();

        foreach (Slot slot in slots)
        {
            if (slot.CurrentItem != null)
            {
                ItemUI ui = slot.CurrentItem.GetComponent<ItemUI>();
                ids.Add(ui.itemData.itemID);
            }
            else
            {
                ids.Add(""); // kosong
            }
        }

        return ids;
    }

    // ------------------------------ //
    // PREFAB MATCHING UNTUK ITEMDATA //
    // ------------------------------ //
    private GameObject FindPrefabForItem(ItemData data)
    {
        foreach (GameObject prefab in ItemPrefabs)
        {
            ItemUI ui = prefab.GetComponent<ItemUI>();
            if (ui != null && ui.itemData == data)
                return prefab;
        }

        Debug.LogError("Prefab untuk item " + data.itemName + " tidak ditemukan!");
        return null;
    }
}
