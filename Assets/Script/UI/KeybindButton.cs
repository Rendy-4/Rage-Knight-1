using UnityEngine;
using TMPro;
using System;


public class KeybindButton : MonoBehaviour
{
    public string actionName;              
    public TextMeshProUGUI keytext;        

    private bool waitingForInput = false;

    private void Start()
    {
        keytext.text = KeybindManager.Instance.GetKey(actionName).ToString();
    }

    public void OnClickButton()
    {
        waitingForInput = true;
        keytext.text = "Press any key...";
    }

    private void Update()
    {
        if (!waitingForInput)
            return;

        if (Input.anyKeyDown)
        {
            KeyCode newKey = DetectKey();

            if (newKey == KeyCode.None)
                return;

            if (KeybindManager.Instance.IsKeyAlreadyUsed(newKey, actionName))
            {
                // jika ingin popup → tinggal panggil popup.Show();
                Debug.Log("Key sudah dipakai!");
                keytext.text = KeybindManager.Instance.GetKey(actionName).ToString();
                waitingForInput = false;
                return;
            }

            KeybindManager.Instance.SetKey(actionName, newKey);
            keytext.text = newKey.ToString();
            waitingForInput = false;
        }
    }

    private KeyCode DetectKey()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
                return key;
        }
        return KeyCode.None;
    }

    internal void RefreshText()
    {
        keytext.text = KeybindManager.Instance.GetKey(actionName).ToString();
    }
}
