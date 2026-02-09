using UnityEngine;

public class EyesFollowHead_FlipX : MonoBehaviour
{
    public SpriteRenderer headRenderer;   // sprite renderer on head
    public Transform leftEye, rightEye;
    private Vector3 leftOffset, rightOffset;

    void Start()
    {
        // store initial local offsets relative to head
        leftOffset = headToLocal(leftEye.position);
        rightOffset = headToLocal(rightEye.position);
    }

    // helper -> position relative to head transform (local)
    Vector3 headToLocal(Vector3 worldPos)
    {
        return Quaternion.Inverse(transform.rotation) * (worldPos - transform.position);
    }

    void LateUpdate()
    {
        bool flipped = headRenderer.flipX;
        // when flipped, invert x of offsets
        Vector3 leftTargetLocal = flipped ? new Vector3(-leftOffset.x, leftOffset.y, leftOffset.z) : leftOffset;
        Vector3 rightTargetLocal = flipped ? new Vector3(-rightOffset.x, rightOffset.y, rightOffset.z) : rightOffset;

        // apply to eye transforms (assuming eyes are direct children of head or enemy root)
        leftEye.localPosition = leftTargetLocal;
        rightEye.localPosition = rightTargetLocal;
    }
}
