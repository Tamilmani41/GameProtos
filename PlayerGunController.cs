using System.Collections;
using UnityEngine;
using System;

public class PlayerGunController : MonoBehaviour
{
    //reference to the game objects
    [Header("Sprite")]
    public GameObject gun_sprite;
    public Object_pool bullet_pool;// reference to bullet pool
                                   //reference to sprite renderer
    public SpriteRenderer spriteRenderer;

    // pivot shoulder sprite and bullet spawn point
    [Header("Gunpivot")]
    public GameObject shoulder;
    public GameObject Gun_pivot;
    public Transform fire_point;
    // equip and speed of bullet
    [Header("Bullet")]
    public float bullet_speed = 10f;
    private bool has_gun = false;
    //Reloading logic
    [Header("Reload")]
    public BulletData data;
    private int bullet_count;
    public bool is_reloading = false;

    [Header("Audio")]
    public AudioSource shoot;
    public AudioClip reload;

    [Header("Muzzle flash")]
    public Sprite[] muzzle;                     
    public SpriteRenderer muzzleFlash;
    private float Flash_time = 0.5f;

    //Exposing ammo info from gun to other scripts
    public int CurrentAmmo => bullet_count;
    public int MaxAmmo => data.mag_capacity;
    // Event to notify ammo changes
    public event Action<int, int> OnAmmoChanged;

    private void Start()
    {
        bullet_count = data.mag_capacity;
        OnAmmoChanged?.Invoke(bullet_count, data.mag_capacity);
    }
    private void Update()
    {
        // check if player has gun if so enable gun
        if(has_gun)
        {
            Aim_atMouse();
            if (is_reloading)
                return;

            
            if (Input.GetMouseButtonDown(0) && bullet_count > 0)
            {
                Shoot();
                bullet_count --;
                OnAmmoChanged?.Invoke(bullet_count, data.mag_capacity);

                if (bullet_count <= 0)
                {
                    StartCoroutine(Reload());
                }
            }
            if (bullet_count < data.mag_capacity && Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
        }
    }

    // Has gun enable gun holding animation
    public void Equip_gun()
    {
        has_gun = true;
        gun_sprite.SetActive(true);
        shoulder.SetActive(true);
    }

    // Make the player aim at mouse position & refer notes
    public void Aim_atMouse()
    {
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouse_pos - Gun_pivot.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Gun_pivot.transform.rotation = Quaternion.Euler(0, 0, angle);
        spriteRenderer.flipX = mouse_pos.x < transform.position.x;
    }

    // Script to fire the gun.
    private void Shoot()
    {
        // Acess bullet pool 
        GameObject bullet = bullet_pool.GetPooledObject();
        
        // To decide the bullet owner...
        Bullets b = bullet.GetComponent<Bullets>();
        b.owner = BulletOwner.Player;
        //Debug.Log("Owner :"+ b.owner);

        bullet.transform.position = fire_point.position;
        bullet.transform.rotation = fire_point.rotation;    // set the rotation of the fire_point to bullet_pool
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = fire_point.right * bullet_speed; // refer notes
        }
        shoot.PlayOneShot(shoot.clip);

        // set muzzle random muzzle flash on gun
        Sprite Selected_flash = muzzle[UnityEngine.Random.Range(0, muzzle.Length)];
        muzzleFlash.sprite = Selected_flash;
        muzzleFlash.enabled = false;
        StartCoroutine(Flash_hide());
    }
    IEnumerator Reload()
    {
        if (is_reloading) yield break;
        is_reloading = true;
        shoot.PlayOneShot(reload);
        yield return new WaitForSeconds(data.reload_time);
        bullet_count = data.mag_capacity;
        OnAmmoChanged?.Invoke(bullet_count, data.mag_capacity);
        is_reloading = false;
    }

    IEnumerator Flash_hide()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(Flash_time);
        muzzleFlash.enabled = false;
    }
}
