using UnityEngine;
using TMPro;

public class CurencyUI : MonoBehaviour
{
    public TextMeshProUGUI curencyText;
    void Update()
    {
        curencyText.text = PlayerCurency.Instance.Coins.ToString();
    }
}
