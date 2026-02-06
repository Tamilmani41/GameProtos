using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_bounds : MonoBehaviour
{
    public Transform bottomLeft;
    public Transform topRight;

    // Start is called before the first frame update
    void Start()
    {
        float width = topRight.position.y - bottomLeft.position.x;
        float height = topRight.position.y - bottomLeft.position.y;
        Debug.Log("level W = " + width + "units");
        Debug.Log("level H = " + height + "units");
    }

}
