using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Vector3 target;
    private Vector3 initialPosition;
    public CharacterController characterController;
    public bool touchingBlade = false;
    private SpriteRenderer spriteRenderer;
    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        NextTarget();
    }
    public void NextTarget()
    {
        float range = Random.Range(-1.0f, 1.0f) * 5.0f;
        target =
            new Vector3(initialPosition.x + range, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        Debug.DrawLine(transform.position, target);
        if (touchingBlade)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = new Color(0x5F / 255.0f, 0x5E / 255.0f, 0xC5 / 255.0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Blade")
            touchingBlade = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Blade")
            touchingBlade = false;
    }
}
