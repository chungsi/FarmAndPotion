using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Rigidbody rb;

    private Vector2 movement;

    void Update()
    {
        // Process input.
        movement.x = Input.GetAxis("Horizontal"); // gives -1 to 1
        movement.y = Input.GetAxis("Vertical");
        movement.Normalize();

        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.y * moveSpeed);
    }

   // void FixedUpdate()
    //{
        // Actual movement stuff.
       // rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        // transform.Translate(new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.fixedDeltaTime);
   // }
}
