using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Vector3 target;
    private Vector3 initialPosition;
    public CharacterController characterController;
    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
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
    }
}
