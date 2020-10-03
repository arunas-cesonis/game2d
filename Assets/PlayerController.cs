using System.Collections;
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

        if (!grounded)
        {
            velocity = velocity + gravity * dt;
        }
        else
        {
            if (jump)
            {
                jump = false;
                grounded = false;
                velocity = velocity + Vector2.up * 10.0f;
            }
    
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

        bool collided = false;
        if (d.y < 0.0f || grounded)
        {
            Vector2 start = new Vector2(bounds.min.x + margin, bounds.min.y);
            Vector2 end = new Vector2(bounds.max.x - margin, bounds.min.y);
            float distance = Mathf.Abs(d.y);

            for (int i = 0; i < vrays; i++)
            {
                float amount = (float)i / (float)(vrays - 1);
                Vector2 origin = Vector2.Lerp(start, end, amount);
                RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, distance, groundMask);
                Debug.DrawRay(origin, Vector2.down, Color.red);
                if (hit.collider)
                {
                    d = new Vector2(d.x, -hit.distance);
                    velocity = new Vector2(velocity.x, 0.0f);
                    grounded = true;
                    collided = true;
                    break;
                }
            }
        }
        if (!collided)
        {
            grounded = false;
        }

        if (d.y > 0.0f)
        {
            Vector2 start = new Vector2(bounds.min.x + margin, bounds.max.y);
            Vector2 end = new Vector2(bounds.max.x - margin, bounds.max.y);
            float distance = Mathf.Abs(d.y);

            for (int i = 0; i < vrays; i++)
            {
                float amount = (float)i / (float)(vrays - 1);
                Vector2 origin = Vector2.Lerp(start, end, amount);
                RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, distance, groundMask);
                Debug.DrawRay(origin, Vector2.up, Color.red);
                if (hit.collider)
                {
                    d = new Vector2(d.x, hit.distance);
                    velocity = new Vector2(velocity.x, 0.0f);
                    grounded = true;
                    break;
                }
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
