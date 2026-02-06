using UnityEngine;

public class Platform_movement : MonoBehaviour
{
    public Transform positionA;
    public Transform positionB;
    public float speed = 8f;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = positionB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            target = target == positionA.position ? positionB.position : positionA.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    //Make player child of the gameobject to make the player stick to the platform.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    //Take out the child object of player when not on the platform.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
