using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController: MonoBehaviour
{
    private LayerMask layerMask;
    private BoxCollider2D boxCollider;
    private bool grounded = false;
    private Vector2 velocity = Vector2.zero;
    public Vector2 gravity = new Vector3(0.0f, -80.0f);
    public float jumpForce = 26.0f;
    public float walkForce = 6.0f;
    private float margin = 0.01f;
    private int vrays = 4;
    private int hrays = 4;
    private bool jump = false;
    private float move = 0.0f;
    private bool facingRight = true;

    public bool IsFacingRight()
    {
        return facingRight;
    }

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }
    public void Jump()
    {
        if (grounded)
        {
            jump = true;
        }
    }
    public void Move(float move)
    {
        this.move = Mathf.Clamp(move, -1.0f, 1.0f);
    }
    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        Bounds bounds = boxCollider.bounds;

        velocity = velocity + gravity * dt;

        if (jump)
        {
            jump = false;
            grounded = false;
            velocity = velocity + Vector2.up * jumpForce;
        }

        Vector2 d = velocity * dt;

        d = d + walkForce * move * Vector2.right * dt;

        boxCollider.enabled = false;

        if (Mathf.Abs(d.y) > 0.0f)
        {
            bool movingDown = d.y < 0.0f;
            Vector2 start = new Vector2(bounds.min.x + margin, movingDown ? bounds.min.y : bounds.max.y);
            Vector2 end = new Vector2(bounds.max.x - margin, movingDown ? bounds.min.y : bounds.max.y);
            Vector2 direction = movingDown ? Vector2.down : Vector2.up;

            bool touchingGround = false;

            for (int i = 0; i < vrays; i++)
            {
                float amount = (float)i / (float)(vrays - 1);
                Vector2 origin = Vector2.Lerp(start, end, amount);
                float distance = Mathf.Abs(d.y);
                RaycastHit2D hit = Util.Raycast(origin, direction, distance, layerMask);
                if (hit.collider)
                {
                    d = new Vector2(d.x, movingDown ? -hit.distance : hit.distance);
                    velocity = new Vector2(velocity.x, 0.0f);
                    if (movingDown)
                    {
                        grounded = true;
                        touchingGround = true;
                    }
                    break;
                }
            }
            if (!touchingGround && movingDown)
            {
                grounded = false;
            }
        }

        if (Mathf.Abs(d.x) > 0.0f)
        {
            bool movingRight = d.x > 0.0f;
            facingRight = movingRight;

            Vector2 start = new Vector2(movingRight ? bounds.max.x : bounds.min.x, bounds.min.y + margin);
            Vector2 end = new Vector2(movingRight ? bounds.max.x : bounds.min.x, bounds.max.y - margin);
            Vector2 direction = movingRight ? Vector2.right : Vector2.left;
            float distance = Mathf.Abs(d.x);

            for (int i = 0; i < hrays; i++)
            {
                float amount = (float)i / (float)(hrays - 1);
                Vector2 origin = Vector2.Lerp(start, end, amount);
                RaycastHit2D hit = Util.Raycast(origin, direction, distance, layerMask);
                if (hit.collider)
                {
                    d = new Vector2(movingRight ? hit.distance : -hit.distance, d.y);
                    velocity = new Vector2(0.0f, velocity.y);
                    break;
                }
            }
        }

        boxCollider.enabled = true;

        transform.Translate(d);
        Vector3 ls = transform.localScale;
        ls.x = facingRight ? 1 : -1;
        transform.localScale = ls;
    }
}