using System;
using UnityEngine;

public class Palyer_movement : MonoBehaviour
{
    // player movement in x axis and jump...
    [Header("Movement")]
    public Rigidbody2D rb;
    public float speed = 3f;

    [Header("Jump")]
    public float Jump_force = 5f;
    public LayerMask Ground_layer;
    public Transform ground_check;
    public float Ground_checkR = 0.2f;
    public bool isgrounded;

    [Header("Gravity")]
    public float Fallforce = 2.5f;

    // added for jump delay in fixed update
    [Header("Buffer")]
    bool jumped_buff;
    float jumpTimer_buff = 0.2f;
    float jump_counter_buff;

    [Header("Gun sprite flip")]
    bool facingRight;
    public SpriteRenderer shoulder;
    SpriteRenderer sp;
    public SpriteRenderer gun;
    public Transform Gun;
    public Transform fire;

    [Header("Gun position")]
    public float X_position = 0.40f,Y_position_R = 0.058f , Y_position_L = -0.06f;
    public float X_mfire = 0.52f, Y_mfire_R = 0.058f, Y_mfire_L = -0.081f;

    [Header("Animation")]
    [SerializeField] private new Animator animation;

    private void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        animation = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Jump"))
        {
            jumped_buff = true;
            jump_counter_buff = jumpTimer_buff;
        }

        if (jump_counter_buff > 0)
            jump_counter_buff -= Time.deltaTime;

        else
            jumped_buff = false;

        if (mouse_pos.x > transform.position.x)
            facingRight = true;
        else
            facingRight = false;

        //sp.flipX = !facingRight;        //flip player sprite , replacing in Aim at mouse
        shoulder.flipY = !facingRight;  //flip shoulder sprite
        //gun.flipY = !facingRight;       flip gun sprite, replace in if
        if (facingRight)
        {
            Gun.localPosition = new Vector3(X_position, Y_position_R, 0);
            fire.localPosition = new Vector3(X_mfire, Y_mfire_R, 0);
            gun.flipY = false;
        }
        else
        {
            Gun.localPosition = new Vector3(X_position, Y_position_L, 0);
            fire.localPosition = new Vector3(X_mfire, Y_mfire_L, 0);
            gun.flipY = true;
        }
    }
    void FixedUpdate()
    {
        // move the character 
        float move = Input.GetAxisRaw("Horizontal");
        if (move > 0)
            sp.flipX = false;

        else if (move < 0)
            sp.flipX = true;
        
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if(animation != null)
        {
            if (move != 0)
                animation.SetBool("is_running", true);
            else
                animation.SetBool("is_running", false);
        }

            // jump only when standing on the ground.
            isgrounded = Physics2D.OverlapCircle(ground_check.position, Ground_checkR, Ground_layer);

        if (jumped_buff && isgrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, Jump_force);
            jumped_buff = false;
        }
        if (isgrounded)
            animation.SetBool("is_jumping", false);
        else
            animation.SetBool("is_jumping", true);

        // doubling the gravity
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (Fallforce - 1) * Time.fixedDeltaTime;       
    }

    // function used to display overlap circle in the inspector
    private void OnDrawGizmosSelected()
    {
        if (ground_check != null)
            Gizmos.DrawWireSphere(ground_check.position, Ground_checkR);
    }
}
