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
        if(data.inventoryData == null || data.inventoryData.savedItemIDs.Count == 0)
        {
            Debug.Log("Inventory Kosong atau belum pernah disave.");
            return;
        }

        inventory.ClearInventory();
        inventory.LoadInventory(data.inventoryData.savedItemIDs);
    }

    public void SaveData(ref GameData data)
    {
        
        data.inventoryData.savedItemIDs = inventory.GetAllItemIDs();
    }
}
