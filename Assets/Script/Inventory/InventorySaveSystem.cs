using UnityEngine;

public class InventorySaveSystem : MonoBehaviour, IDataPresistence
{
    public InventoryController inventory;

    private void Awake()
    {
        inventory = GetComponent<InventoryController>();
    }

    public void LoadData(GameData data)
    {
        if (data.inventoryData.savedSlots.Count == 0)
        {
            Debug.Log("Inventory kosong / belum pernah save.");
            return;
        }

        inventory.ClearInventory();
        inventory.LoadInventory(data.inventoryData.savedSlots);
    }

    public void SaveData(ref GameData data)
    {
        data.inventoryData.savedSlots.Clear();

        var slots = inventory.GetAllSlots();

        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];

            if (slot.currentItem != null)
            {
                ItemUI ui = slot.currentItem.GetComponent<ItemUI>();
                data.inventoryData.savedSlots.Add(
                    new SavedSlotData(i, ui.itemData.itemID, ui.amount)
                );
            }
        }
    }
}
