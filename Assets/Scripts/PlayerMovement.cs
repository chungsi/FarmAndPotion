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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Process input.
        movement.x = Input.GetAxisRaw("Horizontal"); // gives -1 to 1
        movement.z = Input.GetAxisRaw("Vertical");
        // movement.y = 0; // always zero??? maybe not if there are slopes are such
        movement.Normalize();
    }

   void FixedUpdate()
    {
        // Actual movement stuff for rigid bodies are calculated here.
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
