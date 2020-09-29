using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundMask;
    private BoxCollider2D boxCollider;
    private bool isGrounded = false;
    private Vector2 velocity = Vector2.zero;
    private Vector2 gravity = new Vector3(0.0f, -10.0f);
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        if (!isGrounded && Input.GetKey(KeyCode.S))
        {
            velocity = velocity + gravity * Time.fixedDeltaTime;
        }
        Vector2 d = velocity * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            d = d + new Vector2(Time.fixedDeltaTime * 2.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            d = d + new Vector2(-Time.fixedDeltaTime * 2.0f, 0.0f);
        }

        if (d.y < 0.0)
        {
 
            // ray bottom left -> down
            {
                Vector2 ray = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y);
                Vector2 dir = Vector2.down;
                float dist = Mathf.Abs(d.y);

                RaycastHit2D hit = Physics2D.Raycast(ray, dir, dist, groundMask);
                Debug.DrawRay(ray, dir * dist, Color.red);

                if (hit.collider)
                {
                    velocity = new Vector2(velocity.x, 0.0f);
                    d = new Vector2(d.x, Mathf.Sign(d.y) * hit.distance);

                    isGrounded = true;
                }
            }

            // ray bottom right -> down
            {
                Vector2 ray = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
                Vector2 dir = Vector2.down;
                float dist = Mathf.Abs(d.y);

                RaycastHit2D hit = Physics2D.Raycast(ray, dir, dist, groundMask);
                Debug.DrawRay(ray, dir * dist, Color.red);

                if (hit.collider)
                {
                    velocity = new Vector2(velocity.x, 0.0f);
                    d = new Vector2(d.x, Mathf.Sign(d.y) * hit.distance);
                    isGrounded = true;
                }
            }
        }
        if (d.x > 0.0)
        {

            // ray top right -> right
            {
                Vector2 ray = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.max.y);
                Vector2 dir = Vector2.right;
                float dist = Mathf.Abs(d.x);

                RaycastHit2D hit = Physics2D.Raycast(ray, dir, dist, groundMask);
                Debug.DrawRay(ray, dir, Color.red);

                if (hit.collider)
                {
                    print(hit.distance);
                    d = new Vector2(Mathf.Sign(d.x) * hit.distance, d.y);
                }
            }

            // ray bottom right -> right
            {
                Vector2 ray = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y);
                Vector2 dir = Vector2.right;
                float dist = Mathf.Abs(d.x);

                RaycastHit2D hit = Physics2D.Raycast(ray, dir, dist, groundMask);
                Debug.DrawRay(ray, dir, Color.red);

                if (hit.collider)
                {
                    d = new Vector2(Mathf.Sign(d.x) * hit.distance, d.y);
                }
            }
        }
        if (d.x < 0.0)
        {

            // ray top left -> left
            {
                Vector2 ray = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.max.y);
                Vector2 dir = Vector2.left;
                float dist = Mathf.Abs(d.x);

                RaycastHit2D hit = Physics2D.Raycast(ray, dir, dist, groundMask);
                Debug.DrawRay(ray, dir, Color.red);

                if (hit.collider)
                {
                    print(hit.distance);
                    d = new Vector2(Mathf.Sign(d.x) * hit.distance, d.y);
                }
            }

            // ray bottom left -> left
            {
                Vector2 ray = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y);
                Vector2 dir = Vector2.left;
                float dist = Mathf.Abs(d.x);

                RaycastHit2D hit = Physics2D.Raycast(ray, dir, dist, groundMask);
                Debug.DrawRay(ray, dir, Color.red);

                if (hit.collider)
                {
                    d = new Vector2(Mathf.Sign(d.x) * hit.distance, d.y);
                }
            }
        }


        transform.position = transform.position + new Vector3(d.x, d.y, 0.0f);
    }
}
