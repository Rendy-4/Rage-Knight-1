using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ExpUi : MonoBehaviour
{
    public Image expFillImage;      
    public TMP_Text levelText;      
    private PlayerExperience player;

    void Start()
    {
        player = PlayerExperience.Instance;

        if(player == null)
        {
        Debug.LogError("PlayerExperience Tidak Ditemukan!");
        return;
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        if (player == null) return;

        float fill = player.currentExp / player.expToNextLevel;
        expFillImage.fillAmount = fill;

        levelText.text = "Level " + player.level;
    }
}
