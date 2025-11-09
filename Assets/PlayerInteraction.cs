using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    private MarketInteractable nearbymarket;
    private bool IsMarketOpen = false;
    void Update()
    {
        if (nearbymarket != null && nearbymarket.isPlayerNear)
        {
           
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Toggle buka/tutup
                if (!IsMarketOpen)
                {
                    MarketUI.Instance.OpenMarket();
                    nearbymarket.HideText();
                }
                else
                {
                    MarketUI.Instance.CloseMarket();
                    nearbymarket.ShowText();
                }

                IsMarketOpen = !IsMarketOpen;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var market = collision.GetComponent<MarketInteractable>();
        if (market != null)
        {
            nearbymarket = market;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        var market = collision.GetComponent<MarketInteractable>();
        if (market == nearbymarket)
        {
            nearbymarket = null;
        }
    }
}
