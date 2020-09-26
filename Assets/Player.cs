using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer; 
    public Sprite[] sprites;
    private Rigidbody2D rb;
    private int direction = 0;
    private bool grounded = false;
    public float jumpForce;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = 1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
                direction = 1;
            else
                direction = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
                direction = -1;
            else
                direction = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            print("jumpForce " + jumpForce);
            rb.AddForce(new Vector2(0.0f, jumpForce));
            grounded = false;
        }
    }
    void FixedUpdate()
    {
        Vector2 velocity = new Vector2(direction * 4, rb.velocity.y);
        rb.velocity = velocity;
    }


    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
