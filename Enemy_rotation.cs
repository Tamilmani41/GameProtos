using UnityEngine;

public class Enemy_rotation : MonoBehaviour
{
    private SpriteRenderer[] sp;
    private bool facingRight = false;
    public Transform fire_point;

    private void Start()
    {
        sp = GetComponentsInChildren<SpriteRenderer>(); // important - Components s don't forget 's'
    }
    public void Flip(bool faceRight)
    {
        if (facingRight == faceRight) return;

        facingRight = faceRight;
        foreach (SpriteRenderer sr in sp)
        {
            sr.flipX = !sr.flipX;
        }
        //fire_point.localScale = new Vector2(-fire_point.localScale.x, fire_point.localScale.y);
    }
}
