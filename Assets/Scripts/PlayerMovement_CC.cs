using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_CC : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    public CharacterController controller;

    void Update()
    {
        // Process input.
        float horizontal = Input.GetAxisRaw("Horizontal"); // gives -1 to 1
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // This variable gets the angle the player wants to move towards, in radians.
            // We will want to use this to determine what animation sprite to display later.
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            controller.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
}
