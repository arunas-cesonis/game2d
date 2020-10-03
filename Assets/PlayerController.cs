﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundMask;
    private BoxCollider2D boxCollider;
    private bool grounded = false;
    private Vector2 velocity = Vector2.zero;
    private Vector2 gravity = new Vector3(0.0f, -30.0f);
    private float margin = 0.01f;
    private int vrays = 4;
    private int hrays = 4;
    private bool jump = false;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        Bounds bounds = boxCollider.bounds;

        velocity = velocity + gravity * dt;

        if (jump)
        {
            jump = false;
            grounded = false;
            velocity = velocity + Vector2.up * 10.0f;
        }

        Vector2 d = velocity * dt;
        if (Input.GetKey(KeyCode.A))
        {
            d = d + 4.0f * Vector2.left * dt;
        }
        if (Input.GetKey(KeyCode.D))
        {
            d = d + 4.0f * Vector2.right * dt;
        }

        if (Mathf.Abs(d.y) > 0.0f)
        {
            bool movingDown = d.y < 0.0f;
            Vector2 start = new Vector2(bounds.min.x + margin, movingDown ? bounds.min.y : bounds.max.y);
            Vector2 end = new Vector2(bounds.max.x - margin, movingDown ? bounds.min.y : bounds.max.y);
            Vector2 direction = movingDown ? Vector2.down : Vector2.up;
            float distance = Mathf.Abs(d.y);
            bool gotGrounded = false;

            for (int i = 0; i < vrays; i++)
            {
                float amount = (float)i / (float)(vrays - 1);
                Vector2 origin = Vector2.Lerp(start, end, amount);
                RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, groundMask);
                Debug.DrawRay(origin, direction, Color.red);
                if (hit.collider)
                {
                    d = new Vector2(d.x, movingDown ? -hit.distance : hit.distance);
                    velocity = new Vector2(velocity.x, 0.0f);
                    if (movingDown)
                    {
                        grounded = true;
                        gotGrounded = true;
                    }
                    break;
                }
            }
            if (!gotGrounded && movingDown)
            {
                grounded = false;
            }
        }


        if (Mathf.Abs(d.x) > 0.0f)
        {
            bool movingRight = d.x > 0.0f;
          
            Vector2 start = new Vector2(movingRight ? bounds.max.x : bounds.min.x, bounds.min.y + margin);
            Vector2 end = new Vector2(movingRight ? bounds.max.x : bounds.min.x, bounds.max.y - margin);
            Vector2 direction = movingRight ? Vector2.right : Vector2.left;
            float distance = Mathf.Abs(d.x);

            for (int i = 0; i < hrays; i++)
            {
                float amount = (float)i / (float)(hrays - 1);
                Vector2 origin = Vector2.Lerp(start, end, amount);
                RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, groundMask);
                Debug.DrawRay(origin, direction, Color.red);
                if (hit.collider)
                {
                    d = new Vector2(movingRight ? hit.distance : -hit.distance, d.y);
                    velocity = new Vector2(0.0f, velocity.y);
                    break;
                }
            }
        }


        transform.Translate(d);

    }
    private void LateUpdate()
    {
    

    }
}
