using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;

    public Animator anim;
    public PlayerMovement playerMovement;
    public Transform DamagePopup;
    private PlayerStats stats;

    public float cooldown = 2f;
    private float timer;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }
    void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0f) 
        {
            anim.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }

    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    public void DealDamage()
    {
        int attackDamage = stats.CurrentDamage;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        

        if (enemies.Length > 0)
        {
            FindAnyObjectByType<HitPause>().Stop(0.1f);
            foreach (Collider2D enemy in enemies)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                bool isCriticalHit = Random.Range(0f,100f) < 20f;
                int finalDamage = isCriticalHit ? attackDamage * 2 : attackDamage;
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(finalDamage);

                    Transform popup = Instantiate(DamagePopup, new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1, enemy.transform.position.z), Quaternion.identity);

                    popup.GetComponent<PopupDamage>().Setup(finalDamage, isCriticalHit);
                }
            }

            StartCoroutine(waitForSpawn());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator waitForSpawn()
    {
        yield return null;
    }
}

