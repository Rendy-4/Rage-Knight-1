using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject SlotPrefabs;
    public int SlotCount;
    public GameObject[] ItemPrefabs;
    public static InventoryController Instance;
    private List<Slot> slots = new List<Slot>();

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GenerateSlots();
    }

    //test Func//

    private void GenerateSlots()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            Slot slot = Instantiate(SlotPrefabs, InventoryPanel.transform).GetComponent<Slot>();
            slot.index = i;
            slots.Add(slot);
        }
    }

     public List<Slot> GetSlots()
    {
        return slots;
    }
    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            if (slot.currentItem != null)
            {
                Destroy(slot.currentItem);
                slot.currentItem = null;
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
                continue;
            }

            Slot slot = slots[saved.slotIndex];

            GameObject prefab = FindPrefabForItem(data);
            GameObject item = Instantiate(prefab, slot.transform);

            ItemUI ui = item.GetComponent<ItemUI>();
            ui.Setup(data, saved.amount);

            slot.currentItem = item;
        }
    }

    private GameObject FindPrefabForItem(ItemData data)
    {
        foreach (GameObject prefab in ItemPrefabs)
        {
            ItemUI ui = prefab.GetComponent<ItemUI>();

            if (ui.itemData == data)
                return prefab;
        }
        return null;
    }

    public List<Slot> GetAllSlots()
    {
        return slots;
    }
    public void AddItem(ItemData itemData, int amount = 1)
{
    // 1. CARI ITEM YG SUDAH ADA -> TAMBAHKAN JUMLAH
    foreach (var slot in slots)
    {
        if (slot.currentItem != null)
        {
            var ui = slot.currentItem.GetComponent<ItemUI>();
            if (ui.itemData == itemData)
            {
                ui.amount += amount;
                ui.RefreshAmountText();
                return;
            }
        }
    }

    // 2. CARI SLOT KOSONG -> SPAWN ITEM BARU
    foreach (var slot in slots)
    {
        if (slot.currentItem == null)
        {
            GameObject prefab = FindPrefabForItem(itemData);
            if (prefab == null)
            {
                Debug.LogError("Prefab untuk item: " + itemData.itemID + " tidak ditemukan di ItemPrefabs!");
                return;
            }

            GameObject item = Instantiate(prefab, slot.transform);

            ItemUI ui = item.GetComponent<ItemUI>();
            ui.Setup(itemData, amount);  // ← FIXED

            slot.currentItem = item;
            return;
        }
    }

    Debug.Log("Inventory Full!");
}

    public void RefreshInventoryUI()
    {
        var allitems = InventoryDataManager.Instance.GetAllItems();
    }
}
