using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullets : MonoBehaviour
{
    private Object_pool pool;
    public BulletOwner owner;
    //public int damage = 1;
    public BulletData data;

    public void SetPool(Object_pool assigned_pool)
    {
        pool = assigned_pool;
    }

    // applay data from bulletdata
    public void ApplayData(BulletData data)
    {
        //we need to assign data reference to the bullet, so it can use it later (for example in OnTriggerEnter2D)
        this.data = data;

        CancelInvoke();

        float speed = data.speed;
        float lifetime = data.life_time;
        //Debug.Log("bullet lifetime = " + lifetime);

        Invoke(nameof(ReturnToPool), lifetime);
    }


    //method that says "return to your pool" to the bullet
    private void ReturnToPool()
    {
        if (pool == null)
        {// safety check
            Debug.Log("Bullet pool is null",this);
            gameObject.SetActive(false);
            return;
        }
        pool.ReturnToPool(gameObject);
    }

    // Function used to take damage when hit by a bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Code to ignore frienly fire between player and enemy...
        if (owner == BulletOwner.Player && collision.CompareTag("Player"))
            return;
        if (owner == BulletOwner.Enemy && collision.CompareTag("enemy"))
            return;

        Health h = collision.GetComponent<Health>();
        if (h != null)
        {
            h.TakeDamage(data.damage);
            pool.ReturnToPool(gameObject);
        }
        
    }
     
}
