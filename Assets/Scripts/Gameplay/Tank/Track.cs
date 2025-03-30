using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public bool isGrounded { get; private set; }
    private LayerMask groundMask;
    private Vector3 size;
    private int framecounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        groundMask = LayerMask.GetMask("Terrain");
        size = GetComponent<Renderer>().bounds.size;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (framecounter > 5)
        {
            framecounter = 0;
            isGrounded = IsGrounded();
            // Debug.Log(isGrounded);
        }
        else
            framecounter++;
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, size.y / 2 + 0.3f, groundMask);
    }
}
