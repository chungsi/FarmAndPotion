using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Rigidbody rb;

    private Vector3 movement;

    public Animator animator;
    bool moving;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moving = false;
    }

    void Update()
    {
        // Process input.
        movement.x = Input.GetAxisRaw("Horizontal"); // gives -1 to 1
        movement.z = Input.GetAxisRaw("Vertical");
        // movement.y = 0; // always zero??? maybe not if there are slopes are such
        movement.Normalize();

        if (movement.x != 0 || movement.z != 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        animator.SetBool("Movement", moving);   
    }

    void FixedUpdate()
    {
        // Actual movement stuff for rigid bodies are calculated here.
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
      

