using UnityEngine;

[CreateAssetMenu(menuName ="Item/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite icon;
}