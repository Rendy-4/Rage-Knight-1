using System;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
        if (collision.gameObject.CompareTag("Player") && healthManager != null)
        {
            healthManager.TakeDamage(damage);
        }
    }

    public void Attack()
    {
        Debug.Log("Enemy Attack Triggered");
    }
}
