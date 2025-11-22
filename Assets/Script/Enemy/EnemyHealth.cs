using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

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
