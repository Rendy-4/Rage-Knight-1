using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 10;
    void OnCollisionEnter2D(Collision2D collision)
    {
        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.TakeDamage(damage);
        }
    }
}
