using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Engaging : MonoBehaviour
{
    [SerializeField] private float detectionRange = 3f;
    private Transform player;
    [SerializeField] private Enemy_moment Enemy_script;
    private EnemyGun enemyGun;
    public BulletData data;
    public float next_fire_time = 0f;

    private void Awake()
    {
        Enemy_script = GetComponent<Enemy_moment>();
        enemyGun = GetComponent<EnemyGun>();
        
    }
    private void Start()
    {
        GameObject Playerobj = GameObject.FindWithTag("Player");
        if (Playerobj != null)
            player = Playerobj.transform;
        else
            Debug.Log("Player gameobject is null...");
    }
    private void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                Enemy_script.ChasePlayer(player.position);
                Tryshoot();
            }
            else
            {
                next_fire_time = 0f;
                Enemy_script.StopChasing();
            }
        }
        else 
        {
            Debug.Log("Player transform: null", this);
            return;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    private void Tryshoot()
    {
        if (Time.time < next_fire_time) return;

        next_fire_time = Time.time + data.fire_interval;
        enemyGun.Shoot();
    }
}
