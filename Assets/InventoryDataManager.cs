using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : MonoBehaviour, IDataPresistence
{
    public static InventoryDataManager Instance;

    // Backend Inventory: tempat data item sebenarnya disimpan
    private Dictionary<string, int> itemStorage = new Dictionary<string, int>();  
    // itemID → amount

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // ===============================================================
    //  BACKEND: ADD ITEM
    // ===============================================================
    public void AddItem(string itemID, int amount = 1)
    {
        if (itemStorage.ContainsKey(itemID))
        {
            itemStorage[itemID] += amount;
        }
        else
        {
            itemStorage[itemID] = amount;
        }

        Debug.Log($"[Inventory] Add {itemID} x{amount} (Total: {itemStorage[itemID]})");

        RefreshUIIfOpen();
    }

    // ===============================================================
    //  BACKEND: REMOVE ITEM
    // ===============================================================
    public bool RemoveItem(string itemID, int amount = 1)
    {
        if (!itemStorage.ContainsKey(itemID)) return false;
        if (itemStorage[itemID] < amount) return false;

        itemStorage[itemID] -= amount;

        if (itemStorage[itemID] <= 0)
            itemStorage.Remove(itemID);

        RefreshUIIfOpen();
        return true;
    }

    // ===============================================================
    //  BACKEND: GET ALL ITEMS
    // ===============================================================
    public Dictionary<string, int> GetAllItems()
    {
        return new Dictionary<string, int>(itemStorage);
    }

    // ===============================================================
    //  CLEAR INVENTORY
    // ===============================================================
    public void ClearInventory()
    {
        itemStorage.Clear();
        RefreshUIIfOpen();
    }

    // ===============================================================
    //  AUTO REFRESH UI (HANYA JIKA PANEL DIBUKA)
    // ===============================================================
    private void RefreshUIIfOpen()
    {
        InventoryController controller = FindAnyObjectByType<InventoryController>();

        if (controller != null && controller.InventoryPanel.activeInHierarchy)
        {
            controller.RefreshInventoryUI();
        }
    }

    // ===============================================================
    //  SAVE & LOAD (INTEGRASI DENGAN GameData)
    // ===============================================================
    public void LoadData(GameData data)
    {
        itemStorage = new Dictionary<string, int>();

        if (data.backendInventory != null)
        {
            foreach (var pair in data.backendInventory)
            {
                itemStorage.Add(pair.Key, pair.Value);
            }
        }

        Debug.Log("[InventoryDataManager] Load sukses!");
    }

    public void SaveData(ref GameData data)
    {
        data.backendInventory = new SerializableDictionary<string, int>();

        foreach (var pair in itemStorage)
        {
            data.backendInventory.Add(pair.Key, pair.Value);
        }

        Debug.Log("[InventoryDataManager] Save sukses!");
    }
}
