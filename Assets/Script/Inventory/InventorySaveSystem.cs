using System.Collections.Generic;
using UnityEngine;

public class InventorySaveSystem : MonoBehaviour, IDataPresistence
{
    public InventoryController inventory;

    public void LoadData(GameData data)
    {
        if (data.inventoryData.savedSlots.Count == 0)
        {
            return;
        }

        inventory.ClearInventory();
        inventory.LoadInventory(data.inventoryData.savedSlots);
    }

    public void SaveData(ref GameData data)
    {
        if (data.inventoryData == null)
        {
            data.inventoryData = new InventorySaveData();
        }
        if (data.inventoryData.savedSlots == null)
        {
            data.inventoryData.savedSlots = new List<SavedSlotData>();
        }
        data.inventoryData.savedSlots.Clear();

        var slots = inventory.GetAllSlots();

        foreach (var slot in slots)
        {
            if (slot.currentItem != null)
            {
                ItemUI ui = slot.currentItem.GetComponent<ItemUI>();
                if (ui != null && ui.itemData != null)
                {
                    data.inventoryData.savedSlots.Add(
                        new SavedSlotData(slot.index, ui.itemData.itemID, ui.amount));
                    continue;
                }
            }
        }
    }
}
