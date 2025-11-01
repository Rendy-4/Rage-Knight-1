using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 10;
    public Transform attackPoint;
    public float weaponRange = 0.5f;
    public LayerMask targetLayer;
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, targetLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<HealthManager>().TakeDamage(damage);
        }
    }
}
