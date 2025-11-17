using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class KeybindPopupUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI messageText;

    void Start()
    {
        Hide();
    }

    public void Show(string msg)
    {
        messageText.text = msg;
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    // dipanggil tombol OK
    public void OnClickClose()
    {
        Hide();
    }
}

