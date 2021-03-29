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
    bool movingLeft, movingRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movingLeft = false;
        movingRight = false;
    }

    void Update()
    {
        // Process input.
        movement.x = Input.GetAxisRaw("Horizontal"); // gives -1 to 1
        movement.z = Input.GetAxisRaw("Vertical");
        // movement.y = 0; // always zero??? maybe not if there are slopes are such
        movement.Normalize();

        if (movement.x == -1 || movement.z != 0)
        {
            movingLeft = true;
        }
        else
        {
            movingLeft = false;
        }

        if (movement.x == 1 || movement.z != 0)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }

        animator.SetBool("MoveLeft", movingLeft);
        animator.SetBool("MoveRight", movingRight);
    }

    void FixedUpdate()
    {
        // Actual movement stuff for rigid bodies are calculated here.
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
      

