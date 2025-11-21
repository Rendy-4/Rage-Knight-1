using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    [Header("Exp Reward")]
    public int expReward = 20;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        GetComponent<EnemyMovement>().isDead = true;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            
            LootBag lootBag = GetComponent<LootBag>();
            if (lootBag != null)
            {
                lootBag.InstantiateLoot(transform.position);
            }

            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerExperience>().AddExp(expReward);
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
