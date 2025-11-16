using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
   public static ItemDatabase Instance;
   public List<ItemData> allItems;

   private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public ItemData GetItemByID(string id)
    {
        return allItems.Find(item => item.itemID == id);
    }
}
