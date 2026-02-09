using UnityEngine;

public class Enemy_moment : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isChasing = false;
    [SerializeField]private Vector3 chaseTarget;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Enemy_rotation ER;


    [SerializeField] private Transform PointA;
    [SerializeField] private Transform PointB;
    [SerializeField]private Transform target_point;
    private bool faceRight = false;

    public bool IsFacingRight => faceRight;
    // Level bounds collider to keep enemy within bounds
    [SerializeField] private Collider2D levelBounds;
    private float enemyHalfWidth;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ER = GetComponent<Enemy_rotation>();
        target_point = PointA;
    }
    public void ChasePlayer(Vector3 target)
    {
        isChasing = true;
        chaseTarget = target;
    }
    // cache the enemy half width for level bounds checking
    private void Start()
    {
        enemyHalfWidth = GetComponent<Collider2D>().bounds.extents.x;
        levelBounds = GameObject.FindWithTag("LevelBounds").GetComponent<Collider2D>();
    }

    public void StopChasing()
    {
        isChasing = false;
    }
    private void FixedUpdate()
    {
        if (isChasing)
        {
            Vector2 dir = (chaseTarget - transform.position).normalized;
            rb.velocity = dir * speed;

            faceRight = dir.x > 0;
            ER.Flip(faceRight);
        }
        else
        {
            patrol();
        }
    }
    private void patrol()
    {

        Vector2 Newvelocity = (target_point.position - transform.position).normalized;
        rb.velocity = new Vector2(Newvelocity.x * speed, rb.velocity.y);

        if (Vector2.Distance(transform.position, target_point.position) <= 0.5f)                                            
        {
            faceRight = (target_point.position.x - transform.position.x) > 0;
            ER.Flip(!faceRight);
            target_point = (target_point == PointA) ? PointB : PointA;
        }
        ClampPosition();
    }
    private void ClampPosition()
    {
        if (levelBounds == null) return;

        Bounds bounds = levelBounds.bounds;

        float clampedX = Mathf.Clamp(
            transform.position.x,
            bounds.min.x + enemyHalfWidth,
            bounds.max.x - enemyHalfWidth
        );

        transform.position = new Vector3(
            clampedX,
            transform.position.y,
            transform.position.z
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PointA.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.position, 0.5f);
        Gizmos.DrawLine(PointA.position , PointB.position);
        
    }
}
