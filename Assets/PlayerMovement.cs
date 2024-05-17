using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 movement;
    private string lastDirection;
    private InputAction moveAction;
    private Animator animator;
    private bool controlsEnabled = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (controlsEnabled)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }


        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("IsMoving", false);
        } else
        {
            animator.SetBool("IsMoving", true);
        }
        animator.SetFloat("MoveX", movement.x);
        if (movement.x != 0)
        {
            animator.SetFloat("MoveY", 0);
        }
        else
        {
            animator.SetFloat("MoveY", movement.y);
        }

        updateLastDirection();
        flipOrNotFlip();
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
    private void updateLastDirection()
    {
        if(movement.x< 0)
        {
            lastDirection = "left";
        }else if (movement.x > 0)
        {
            lastDirection = "right";
        }
        else if (movement.y < 0)
        {
            lastDirection = "down";
        }
        else if (movement.y > 0)
        {
            lastDirection = "up";
        }    
    }
    private void flipOrNotFlip()
    {
        if (lastDirection == "left")
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
