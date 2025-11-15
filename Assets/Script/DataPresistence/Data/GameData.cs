using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerHealth;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> EnemyDefeated;
    public InventorySaveData inventoryData; 

    public GameData()
    {
        playerHealth = 100f;
        playerPosition = new Vector3(15.71f, -99.85f, 0f);
        EnemyDefeated = new SerializableDictionary<string, bool>();

        inventoryData = new InventorySaveData(); 
    }
}
[System.Serializable]
public class InventorySaveData
{
    public List<string> savedItemIDs = new List<string>();
}
