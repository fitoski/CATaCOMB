using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private InputAction moveAction;
    private Animator animator;
    private bool controlsEnabled = false;

    void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
        moveAction.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log(movement);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    void FixedUpdate()
    {
        if(controlsEnabled)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        }
        Debug.Log(movement.x);
        Debug.Log(movement.y);
    }

    void Update()
    {
        
    }

    public void EnableControls()
    {
        controlsEnabled = true;
    }

    public void DisableControls()
    {
        controlsEnabled = false;
        movement = Vector2.zero;
    }
}
