using Unity.VisualScripting;
using UnityEngine;

public class Camera_movement : MonoBehaviour
{
    // Renderer to find level height & width
    public BoxCollider2D levelcollider;

    public float LevelMinX, LevelMaxX , LevelMinY , LevelMaxY ;
    float minX, maxX, minY, maxY;
    // variables to access player & camera 
    public Transform player;
    public Vector3 offset;
    public float smooth_speed = 0.125f;
    float vertExt;
    float horzExt;
    Camera cam;

    private void Start()
    {
        // Accessimg main camera 
        cam = Camera.main;
        // Getting bounds using bounding box
        Bounds bounds = levelcollider.bounds;
        // Gathering the the bounding value
        LevelMinX = bounds.min.x;
        LevelMaxX = bounds.max.x;
        LevelMinY = bounds.min.y;
        LevelMaxY = bounds.max.y;
    }
    // updates after update()
    private void LateUpdate()
    {


        // Calculating he cameras height & width
        vertExt = cam.orthographicSize;
        horzExt = vertExt * cam.aspect;
        float minX = LevelMinX + horzExt;
        float maxX = LevelMaxX - horzExt;
        float minY = LevelMinY + vertExt;
        float maxY = LevelMaxY - vertExt;
        // follow player smoothly around the map...
        Vector3 desired_pos = player.position + offset;

        // Calculating the clamping position of the camera.
        float ClampedX = Mathf.Clamp(desired_pos.x, minX, maxX);
        float ClampedY = Mathf.Clamp(desired_pos.y, minY, maxY);

        // These lines are used by me when I am only using it for the smooth camera.I will leave it like a memory holder.
        //Vector3 Smooth_pos = Vector3.Lerp(transform.position, desired_pos, smooth_speed);
        //transform.position = new Vector3(Smooth_pos.x, Smooth_pos.y, transform.position.z);

        // set the camera boundries.
        Vector3 ClampedPosition = new Vector3(ClampedX ,ClampedY ,transform.position.z);
        transform.position = Vector3.Lerp(transform.position ,ClampedPosition ,smooth_speed);
    }
}
// Days worked on this script - 6 freaking days