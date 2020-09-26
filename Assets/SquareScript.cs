using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScript : MonoBehaviour
{
    // Start isp called before the first frame update
    Animator animator;
    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            animator.SetTrigger("To1");
        if (Input.GetKeyUp(KeyCode.S))
            animator.SetTrigger("To2");
        
    }
}
