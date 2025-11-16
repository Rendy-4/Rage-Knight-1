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
        //Test Code 
        ItemData wood = ItemDatabase.Instance.GetItemByID("wood_003");
        ItemData Rock = ItemDatabase.Instance.GetItemByID("rock_002");
        SpawnTestItem(0, wood, 8);
        SpawnTestItem(1, wood, 2);
        SpawnTestItem(2, Rock, 2);

    }

    //test Func//
    private void SpawnTestItem(int index, ItemData data, int amount)
{
    if (data == null)
    {
        Debug.LogError("ItemData NULL saat spawn!");
        return;
    }

    Slot slot = slots[index];

    GameObject prefab = FindPrefabForItem(data);
    if (prefab == null)
    {
        Debug.LogError("Prefab NULL untuk item: " + data.itemID);
        return;
    }

    GameObject item = Instantiate(prefab, slot.transform);
    ItemUI ui = item.GetComponent<ItemUI>();

    ui.Setup(data, amount);

    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    slot.currentItem = item;
}

    //test Func//

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

        Debug.Log(
            $"Check prefab {prefab.name}: prefab.itemDataID={ui.itemData?.itemID}, " +
            $"searchID={data?.itemID}, SAME? {ui.itemData == data}"
        );

        if (ui != null && ui.itemData == data)
            return prefab;
    }

    Debug.LogError("Prefab NOT FOUND untuk item: " + data.itemID);
    return null;
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
                Debug.LogWarning("Item ID tidak ditemukan: " + saved.itemID);
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

    public List<Slot> GetAllSlots()
    {
        return slots;
    }
    public void AddItem(ItemData data, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (slot.currentItem != null)
            {
                var ui = slot.currentItem.GetComponent<ItemUI>();
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
            if (slot.currentItem == null)
            {
                GameObject prefab = FindPrefabForItem(data);
                GameObject item = Instantiate(prefab, slot.transform);

                ItemUI ui = item.GetComponent<ItemUI>();
                ui.Setup(data, amount);

                slot.currentItem = item;
                return; 
            }
        }
        Debug.Log("Inventory Full!");
    }
}
