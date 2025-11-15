using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemData itemData;

    public void Setup(ItemData data)
    {
        itemData = data;

        GetComponent<Image>().sprite = data.icon; //Set icon item otomatis//
    }
}
