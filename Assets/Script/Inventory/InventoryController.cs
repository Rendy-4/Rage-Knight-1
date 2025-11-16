using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject SlotPrefabs;
    public int SlotCount;
    public GameObject[] ItemPrefabs;

    private List<Slot> slots = new List<Slot>();

    void Start()
    {
        GenerateSlots();
    }

    private void GenerateSlots()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            Slot slot = Instantiate(SlotPrefabs, InventoryPanel.transform).GetComponent<Slot>();
            slots.Add(slot);
        }
    }

    private GameObject FindPrefabForItem(ItemData data)
    {
        foreach (GameObject prefab in ItemPrefabs)
        {
            var ui = prefab.GetComponent<ItemUI>();
            if (ui != null && ui.itemData == data)
                return prefab;
        }
        return null;
    }

    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            if (slot.CurrentItem != null)
            {
                Destroy(slot.CurrentItem);
                slot.CurrentItem = null;
            }
        }
    }

    public void LoadInventory(List<SavedSlotData> savedSlots)
    {
        foreach (var saved in savedSlots)
        {
            if (saved.itemID == "")
                continue;

            ItemData data = ItemDatabase.Instance.GetItemByID(saved.itemID);
             if (data == null)

            {
                Debug.LogWarning("Item ID tidak ditemukan: " + saved.itemID);
                continue;
            }

            Slot slot = slots[saved.slotIndex];

            GameObject prefab = FindPrefabForItem(data);
            GameObject item = Instantiate(prefab, slot.transform);

            ItemUI ui = item.GetComponent<ItemUI>();

            ui.Setup(data, saved.amount);
            slot.CurrentItem = item;
        }
    }

    public List<Slot> GetAllSlots()
    {
        return slots;
    }
    public void AddItem(ItemData data, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (slot.CurrentItem != null)
            {
                var ui = slot.CurrentItem.GetComponent<ItemUI>();
                if (ui.itemData == data)
                {
                    ui.amount += amount;
                    ui.RefreshAmountText();
                    return;
                }
            }
        }
        foreach (var slot in slots)
        {
            if (slot.CurrentItem == null)
            {
                GameObject prefab = FindPrefabForItem(data);
                GameObject item = Instantiate(prefab, slot.transform);

                ItemUI ui = item.GetComponent<ItemUI>();
                ui.Setup(data, amount);

                slot.CurrentItem = item;
                return; 
            }
        }
        Debug.Log("Inventory Full!");
    }
}
