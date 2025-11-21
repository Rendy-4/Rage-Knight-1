using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpUIBar : MonoBehaviour
{
   public PlayerExperience PlayerExp;
   public Image xpFill;
   public TMP_Text levelText;

   void Update()
    {
        xpFill.fillAmount = PlayerExp.currentExp / PlayerExp.expToNextLevel;
        levelText.text = "Lv " + PlayerExp.level;
    }
}
