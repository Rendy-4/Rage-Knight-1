using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ItemUI : MonoBehaviour
{
    public ItemData itemData;
    public int amount = 1;

    [Header("UI refs")]
    public Image icon;            
    public TMP_Text amountText;   // optional, assign di prefab; bisa null
    private Image _cachedImage;

    private void Awake()
    {
        if (icon == null)
            _cachedImage = GetComponent<Image>();
        else
            _cachedImage = icon;
    }

    // Setup with amount (default 1)
    public void Setup(ItemData data, int amt = 1)
    {
        itemData = data;
        amount = Mathf.Max(1, amt);

        if (_cachedImage != null && data != null)
            _cachedImage.sprite = data.icon;

        RefreshAmountText();
    }

    public string GetItemID()
    {
        return itemData != null ? itemData.itemID : "";
    }

    public void ChangeAmount(int newAmount)
    {
        amount = Mathf.Max(0, newAmount);
        RefreshAmountText();
    }

    public void AddAmount(int add)
    {
        amount += add;
        amountText.text = amount.ToString();
    }

    public void RefreshAmountText()
    {
        if (amountText == null) return;

        if (itemData != null && amount > 1)
            amountText.text = amount.ToString();
        else
            amountText.text = "";
    }
}
