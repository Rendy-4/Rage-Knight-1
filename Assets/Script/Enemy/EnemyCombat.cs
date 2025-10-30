using System;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.TakeDamage(damage);
        }
    }

    public void Attack()
    {
        // This method can be called via animation events to trigger attack logic
        Debug.Log("Enemy Attack Triggered");
    }
}
