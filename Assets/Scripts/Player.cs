using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.Play("SwordHit 1");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            characterController.Jump();
        }
        float move = 0.0f;
        if (Input.GetKey(KeyCode.A))
        {
            move = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move = 1.0f;
        }
        characterController.Move(move);
    }

}
