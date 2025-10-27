using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image Healthbar;
    public float Maxhealth = 100f;

    private EnemyCombat enemyCombat;

    public void TakeDamage(float damage)
    {
        Maxhealth -= damage;
        Maxhealth = Mathf.Clamp(Maxhealth, 0, 100);
        Healthbar.fillAmount = Maxhealth / 100f;

        if (Maxhealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void Heal(float healAmount)
    {
        Maxhealth += healAmount;
        Maxhealth = Mathf.Clamp(Maxhealth, 0, 100);
        Healthbar.fillAmount = Maxhealth / 100f;
    }
}
