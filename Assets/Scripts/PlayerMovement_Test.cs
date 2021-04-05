using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Test : MonoBehaviour
{
    private Animator animator;

    private int moveRightHash;
    private int moveLeftHash;

    private Vector2 currentMovement;
    private bool isMovementPressed;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Rigidbody rb;


    [Header("Input Settings")]
    public GameInput gameInput;
    private Vector3 rawInputMovement;

    // private void Awake()
    // {
    //     gameInput = new GameInput();
    //     gameInput.Gameplay.Move.performed += ctx => {
    //         currentMovement = ctx.ReadValue<Vector2>();
    //         isMovementPressed = currentMovement.x != 0 || currentMovement.y != 0;
    //     };
    // }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // animator = GetComponent<Animator>();

        // moveRightHash = Animator.StringToHash("moveRight");
        // moveLeftHash = Animator.StringToHash("moveLeft");
    }

    // private void OnEnable()
    // {
    //     gameInput.Gameplay.Move.Enable();
    // }

    // private void OnDisable()
    // {
    //     gameInput.Gameplay.Move.Disable();
    // }

    void Update()
    {
    }

   void FixedUpdate()
    {
        // Actual movement stuff for rigid bodies are calculated here.
        rb.MovePosition(rb.position + rawInputMovement * moveSpeed * Time.fixedDeltaTime);
    }

    // public void handleMovement()
    // {
    //     bool isMoveRight = animator.GetBool(moveRightHash);
    //     bool isMoveLeft = animator.GetBool(moveLeftHash);
    // }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
}
