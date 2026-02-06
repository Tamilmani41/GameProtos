using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Object_pool bullet_pool;

    public BulletData data;
    
    public void Shoot()
    {
        // Get bullet from pooled obj's and fire from the enemy bullet fire point...   
        GameObject bullet = bullet_pool.GetPooledObject();
        
        // To decide the bullet owner...
        Bullets b = bullet.GetComponent<Bullets>();
        b.owner = BulletOwner.Enemy;
        //Debug.Log("owner :"+ b.owner);

        Enemy_moment enemyMovement = GetComponent<Enemy_moment>();

        float dir = enemyMovement.IsFacingRight ? 1f : -1f;

        Vector2 direction = Vector2.right * dir;

        bullet.transform.position = firePoint.position + (Vector3)(direction * 0.3f);

        bullet.transform.right = direction;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * data.speed;
            //Debug.Log("velocity = " + rb.velocity);
        }
        else
        {
            Debug.LogWarning("No Rigidbody2D found on the bullet prefab.");
        }
    }
private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firePoint.position, 0.1f);
    }
}
