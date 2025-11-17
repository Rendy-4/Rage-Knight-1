using UnityEngine;

public class ResetButton : MonoBehaviour
{
   public void ResetToDefault()
{
    KeybindManager.Instance.ResetToDefault();
    // refresh semua UI keybind button
}
}
