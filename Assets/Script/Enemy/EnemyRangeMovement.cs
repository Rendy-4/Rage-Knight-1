using UnityEngine;

public class EnemyRangeMovement : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    private Transform target;
    private EnemyRangeCombat enemyRangeCombat;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    [Header("Movement Settings")]
    public float speed = 3f;
    public float playerDetectRange = 6f;
    private int facingDirection = -1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyRangeCombat = GetComponent<EnemyRangeCombat>();
    }

    void Update()
    {
        CheckForPlayer();

        if (target != null)
            Chase();
        else
            rb.linearVelocity = Vector2.zero;
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget <= enemyRangeCombat.distanceToShoot)
            {
                rb.linearVelocity = Vector2.zero;
                enemyRangeCombat.TryShoot(target); // menembak ke arah player
            }
            else if (distanceToTarget >= enemyRangeCombat.distanceToStop)
            {
                Chase();
            }
        }
        else
        {
            target = null;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void Chase()
    {
        if (target == null) return;

        bool shouldFlipRight = target.position.x > transform.position.x && facingDirection == -1;
        bool shouldFlipLeft = target.position.x < transform.position.x && facingDirection == 1;

        if (shouldFlipRight || shouldFlipLeft)
            Flip();

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}
