
using UnityEngine;

public class MarketUI : MonoBehaviour
{
    public static MarketUI Instance;
    [SerializeField] private GameObject MarketPanel;

    void Awake()
    {
        Instance = this;
    }

    public void OpenMarket()
    {
        MarketPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseMarket()
    {
        MarketPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
