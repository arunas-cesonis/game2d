using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Sword sword;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        sword = GetComponentInChildren<Sword>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            sword.Hit();
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
