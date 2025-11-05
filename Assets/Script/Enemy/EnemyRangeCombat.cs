using UnityEngine;

public class EnemyRangeCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float distanceToShoot = 5f;
    public float distanceToStop = 2.5f;
    public int bulletDamage = 10;
    public float fireRate = 1f; // waktu antar peluru

    [Header("References")]
    public Transform firingPoint;
    public GameObject bulletPrefab;

    private float fireCooldown = 0f;

    void Update()
    {
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    public void TryShoot(Transform target)
    {
        if (fireCooldown > 0f || target == null) return;

        // hitung arah ke player
        Vector2 direction = (target.position - firingPoint.position).normalized;

        // spawn peluru
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // pastikan peluru punya arah & damage
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
            bulletScript.damage = bulletDamage;
        }

        // rotasi peluru sesuai arah
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // reset cooldown
        fireCooldown = 1f / fireRate;
    }
}
