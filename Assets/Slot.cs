using UnityEngine;
using UnityEngine.Rendering;

public class Slot : MonoBehaviour
{
   public GameObject CurrentItem; // current item held in the slot//
   public string SavedItemId ="";
   public void ClearSlot()
{
    if (CurrentItem != null)
        Destroy(CurrentItem);
    CurrentItem = null;
}
}
