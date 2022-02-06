using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator _animator;
    private int _movementHash;
    private Vector2 _prevMovementInput;
    private Vector3 _rawInputMovement;

    private void Start()
    {
        // rb = GetComponent<Rigidbody>();

        // _animator = GetComponent<Animator>();
        _movementHash = Animator.StringToHash("Movement");
    }

    private void OnEnable()
    {
        _inputReader.moveEvent += OnMovement;
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMovement;
    }

   void FixedUpdate()
    {
        // Actual movement stuff for rigid bodies are calculated here.
        rb.MovePosition(rb.position + _rawInputMovement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnMovement(Vector2 _movement)
    {
        _prevMovementInput = _movement;
        _rawInputMovement = new Vector3(_prevMovementInput.x, 0f, _prevMovementInput.y);

        if (_prevMovementInput.x != 0)
            _animator.SetFloat("Horizontal", _prevMovementInput.x);

        _animator.SetFloat("Vertical", _prevMovementInput.y);
        _animator.SetFloat("Speed", _prevMovementInput.sqrMagnitude);
    }
}
