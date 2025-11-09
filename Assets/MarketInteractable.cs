using UnityEngine;

public class MarketInteractable : MonoBehaviour
{
    [SerializeField] private GameObject InteractText;
    private bool IsPlayerNear = false;

    private void Start()
    {
        InteractText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IsPlayerNear = true;
        InteractText.SetActive(true);  
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        IsPlayerNear = false;
        InteractText.SetActive(false);
    }
    public void HideText()
    {
        InteractText.SetActive(false);
    }
    public void ShowText()
    {
        if (isPlayerNear)
        {
            InteractText.SetActive(true);
        }
    }
    public bool isPlayerNear => IsPlayerNear;
}

