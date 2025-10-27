using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform target;
    public float speed = 3;
    private bool isChasing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       if(isChasing == true)
        {
            UnityEngine.Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed; // Mengganti linearVelocity dengan velocity
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isChasing = false;
            rb.linearVelocity = UnityEngine.Vector2.zero; // Mengganti linearVelocity dengan velocity
        }
    }
}
