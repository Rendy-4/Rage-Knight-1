using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();
    
    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();

        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.dropChange)
            {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        else
        {
            return null;
        }
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            float dropForce = Random.Range(5f, 10f);
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
            lootObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }
}
