using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public CharacterController characterController;
    private BoxCollider2D boxCollider;
    public GameObject body;
    public GameObject eye;
    private GameObject target;
    private bool touchingBlade = false;
    private SpriteRenderer spriteRenderer;
    private Sword sword;
    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        spriteRenderer = body.GetComponent<SpriteRenderer>();
        sword = GetComponentInChildren<Sword>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private Vector2 GetDirectionToTarget()
    {
        return (target.transform.position - eye.transform.position);
    }

    private Vector2 GetRaycastDirection()
    {
        if (target)
        {
            return GetDirectionToTarget().normalized;
        }
        else
        {
            return characterController.IsFacingRight() ? Vector2.right : Vector2.left;
        }
    }

    private float GetRaycastDistance()
    {
        if (target)
        {
            return 4.0f;
        }
        else
        {
            return 2.0f;
        }
    }

    private void Update()
    {

        boxCollider.enabled = false;


        Bounds bounds = boxCollider.bounds;
        Vector2 origin = eye.transform.position;
        RaycastHit2D hit = Util.Raycast(origin, GetRaycastDirection(), GetRaycastDistance());

        target = null;
        if (hit.collider)
        {
            if (hit.collider.gameObject.name == "Player")
            {
                target = hit.collider.gameObject;
            }
        }

        boxCollider.enabled = true;

        if (target)
        {
            float move = Mathf.Sign(target.transform.position.x - transform.position.x);
            characterController.Move(move);
            if (GetDirectionToTarget().magnitude < 1.0f)
            {
                sword.Hit();
            }
        }
        else
        {
            characterController.Move(0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        print("Collisiong enter " + collider.gameObject.name);
        if (collider.gameObject.name == "Blade")
        {
            touchingBlade = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        print("Collisiong exit " + collider.gameObject.name);
        if (collider.gameObject.name == "Blade")
        {
            touchingBlade = false;
        }
    }
}
