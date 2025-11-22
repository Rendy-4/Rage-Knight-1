using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public int BaseMaxHP = 100;
    public int BaseDamage = 10;
    [Header("Current Stats")]
    public int CurrentMaxHP;
    public int CurrentDamage;
    [Header("Multiplier Stats")]
    public float MaxHPMultiplier = 1.2f;
    public float DamageMultiplier = 1.1f;

    private PlayerExperience xp;

    void Start()
    {
        xp = GetComponent<PlayerExperience>();
        UpdateStats();
    }

    public void UpdateStats()
    {
        CurrentMaxHP = Mathf.RoundToInt(BaseMaxHP * Mathf.Pow(MaxHPMultiplier, xp.level - 1));
        CurrentDamage = Mathf.RoundToInt(BaseDamage * Mathf.Pow(DamageMultiplier, xp.level - 1));
    }

    public void OnLevelUp()
    {
        UpdateStats();
        Debug.Log("Stats Updated! Attack: " + CurrentDamage + " | HP: " + CurrentMaxHP);
    }
}
