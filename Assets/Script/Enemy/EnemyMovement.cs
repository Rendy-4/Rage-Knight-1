using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private Animator anim;

    public float attackRange = 1;
    public float speed = 3;
    public float attackCooldown = 2f;
    public float playerDetectRange = 5f;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float timerCooldown = 0f;
    private int facingDirection = -1;
    private EnemyState AnimState, newState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        if (timerCooldown > 0)
        {
            timerCooldown -= Time.deltaTime;
        }
        if(AnimState == EnemyState.Chasing && target != null)
        {
            Chase();
        }
        else if(AnimState == EnemyState.Attacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            target = hits[0].transform;

            if (Vector2.Distance(transform.position, target.transform.position) <= attackRange && timerCooldown <= 0)
            {
            timerCooldown = attackCooldown;
            ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, target.transform.position) > attackRange)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    void ChangeState(EnemyState newState)
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isChasing", false);
        anim.SetBool("isAttacking", false);

        AnimState = newState;

        if (AnimState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (AnimState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (AnimState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }

    void Chase()
    {
        if (target.position.x > transform.position.x && facingDirection == -1 || target.position.x < transform.position.x && facingDirection == 1)
        {
                Flip();
        }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}