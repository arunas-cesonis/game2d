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

        Vector2 ray = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y);
        Vector2 dir = Vector2.down;
        float dist = Mathf.Abs(d.y);

        RaycastHit2D hit = Physics2D.Raycast(ray, dir, dist, groundMask);
        Debug.DrawRay(ray, dir * dist, Color.red);

        if (hit.collider && hit.distance > 0.0f)
        {
            velocity = new Vector2(velocity.x, 0.0f);
            d = new Vector2(d.x, dir.y * hit.distance);
            isGrounded = true;
        }

        transform.position = transform.position + new Vector3(d.x, d.y, 0.0f);

    }
}
