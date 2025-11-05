using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifeTime = 3f;
    public LayerMask targetLayer;

    private Vector2 direction;
    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return; // biar gak double hit

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HealthManager hp = collision.GetComponent<HealthManager>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            hasHit = true;
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            hasHit = true;
            Destroy(gameObject);
        }
    }
}
