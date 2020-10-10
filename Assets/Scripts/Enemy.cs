using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Vector3 target;
    public Vector3 initialPosition;
    public void Awake()
    {
        initialPosition = transform.position;
        NextTarget();
    }
    public void NextTarget()
    {
        target = initialPosition + Vector3.right * Random.Range(-1.0f, 1.0f) * 5.0f;
    }
    private void Update()
    {
        Debug.DrawLine(transform.position, target);
    }
}
