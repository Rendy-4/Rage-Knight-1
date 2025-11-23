using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseSlot : MonoBehaviour
{
    [Header("Item yang dijual")]
    public ItemData itemData;
    public int price = 10;

    [Header("UI References")]
    public Image icon;
    public TMP_Text priceText;

    private void Start()
    {
        if (itemData != null && icon != null)
            icon.sprite = itemData.icon;

        if (priceText != null)
            priceText.text = price.ToString();
    }

    private void OnMouseDown()
    {
        TryBuyItem();
    }

    public void TryBuyItem()
    {
        if (itemData == null)
        {
            Debug.LogError("PurchaseSlot: itemData belum di-assign!");
            return;
        }

        // 1. Cek uang cukup
        if (!PlayerCurency.Instance.SpendCoins(price))
        {
            Debug.Log("Uang tidak cukup untuk membeli: " + itemData.itemName);
            return;
        }

        // 2. Tambah item ke backend database
        InventoryDataManager.Instance.AddItem(itemData.itemID, 1);

        // 3. Tambah item ke UI inventory
        InventoryController.Instance.AddItem(itemData, 1);

        Debug.Log("Berhasil membeli: " + itemData.itemName);
    }
}
