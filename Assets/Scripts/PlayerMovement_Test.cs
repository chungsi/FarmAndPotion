using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Test : MonoBehaviour
{
    Animator animator;

    PlayerInput input;
    Vector2 currentMovement;
    bool isMovementPressed;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Rigidbody rb;

    private Vector3 movement;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    private Vector3 rawInputMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

   void FixedUpdate()
    {
        // Actual movement stuff for rigid bodies are calculated here.
        rb.MovePosition(rb.position + rawInputMovement * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
}
