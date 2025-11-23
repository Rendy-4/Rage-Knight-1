using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerHealth;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> EnemyDefeated;
    public InventorySaveData inventoryData; 
    public SerializableDictionary<string, int> backendInventory;
    public SerializableDictionary<String, int> keybinds;
    public float musicVolume;
    public float sfxVolume;
    public int resolutionIndex;
    public bool isFullscreen;

    public int playerLevel;
    public float playerExp;
    public float ExpToNextLevel;
    public int PlayerCoins;


    public GameData()
    {
        playerHealth = 100f;
        playerPosition = new Vector3(15.71f, -99.85f, 0f);
        EnemyDefeated = new SerializableDictionary<string, bool>();

        inventoryData = new InventorySaveData(); 

        musicVolume = 1f;
        sfxVolume = 1f;
        resolutionIndex = 0;
        isFullscreen = true;

        

        keybinds = new SerializableDictionary<string, int>()
        {
            { "Attack", (int)KeyCode.Mouse0 },
            { "Sprint", (int)KeyCode.LeftShift },
            { "Interact", (int)KeyCode.F },

            { "Skill1", (int)KeyCode.Alpha1 },
            { "Skill2", (int)KeyCode.Alpha2 },
            { "Skill3", (int)KeyCode.Alpha3 }
        };

        backendInventory = new SerializableDictionary<string, int>();
    }
}

[Serializable]
public class InventorySaveData
{
    public List<SavedSlotData> savedSlots = new List<SavedSlotData>();
}
[Serializable]
public class SavedSlotData
{
    public int slotIndex;
    public string itemID;
    public int amount;

    public SavedSlotData(int index, string id, int amt)
    {
        slotIndex = index;
        itemID = id;
        amount = amt;
    }
}
