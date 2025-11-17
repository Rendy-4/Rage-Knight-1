using UnityEngine;
using TMPro;

public class KeybindButton : MonoBehaviour
{
    [Header("Action Name (contoh: Attack, Interact)")]
    public string actionName;

    [Header("Reference")]
    public TextMeshProUGUI keytext;   
    public KeybindPopupUI popup;     

    private bool waitingForInput = false;

    void Start()
    {
        if (keytext == null)
            keytext = GetComponentInChildren<TextMeshProUGUI>();

        RefreshText();
    }

    public void OnClickButton()
    {
        waitingForInput = true;
        keytext.text = "Press any key...";
        popup.Show("Press any key!");
    }

    private void OnGUI()
    {
        if (!waitingForInput)
            return;

        Event e = Event.current;

        // ======================
        // INPUT KEYBOARD
        // ======================
        if (e.isKey)
        {
            KeyCode newKey = e.keyCode;

            if (KeybindManager.Instance.IsKeyAlreadyUsed(newKey, actionName))
            {
                popup.Show("Key sudah digunakan!");
                waitingForInput = false;
                RefreshText();
                return;
            }

            KeybindManager.Instance.SetKey(actionName, newKey);
            waitingForInput = false;
            RefreshText();
            popup.Hide();
        }

        // ======================
        // INPUT MOUSE (Mouse0, Mouse1, Mouse2, dst)
        // ======================
        if (e.isMouse)
        {
            KeyCode newKey = KeyCode.Mouse0 + e.button;

            if (KeybindManager.Instance.IsKeyAlreadyUsed(newKey, actionName))
            {
                popup.Show("Mouse button sudah digunakan!");
                waitingForInput = false;
                RefreshText();
                return;
            }

            KeybindManager.Instance.SetKey(actionName, newKey);
            waitingForInput = false;
            RefreshText();
            popup.Hide();
        }
    }

    public void RefreshText()
    {
        keytext.text = KeybindManager.Instance.GetKey(actionName).ToString();
    }
}
