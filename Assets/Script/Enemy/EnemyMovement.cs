using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private Animator anim;

    public float attackRange = 1;
    public float speed = 3;
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
       if(AnimState == EnemyState.Chasing && target != null)
        {
            Chase();
        }
        else if (AnimState == EnemyState.Attacking)
        {
            rb.linearVelocity = UnityEngine.Vector2.zero;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            if (target == null) 
                target = collision.transform;
                
            ChangeState(EnemyState.Chasing);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.linearVelocity = UnityEngine.Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
        
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new UnityEngine.Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
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

    void Chase()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
        {
            ChangeState(EnemyState.Attacking);
        }
        else if (target.position.x > transform.position.x && facingDirection == -1 || target.position.x < transform.position.x && facingDirection == 1)
        {
                Flip();
        }
        UnityEngine.Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}