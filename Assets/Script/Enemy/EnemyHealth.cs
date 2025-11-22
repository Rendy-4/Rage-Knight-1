using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public float expreward = 20;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
       
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
             EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.isDead = true;
            }
            
            LootBag lootBag = GetComponent<LootBag>();
            if (lootBag != null)
            {
                lootBag.InstantiateLoot(transform.position);
            }


            if(PlayerExperience.Instance != null)
            {
                PlayerExperience.Instance.AddExp(expreward);
            }
            
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
