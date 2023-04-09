using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class MetroidPlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;

    Vector2 moveInput;
    TouchingDirections touchingDirections;

    static string yVelocity = "yVelocity";
    static string jumpTrigger = "Jump";
    static string attackTrigger = "attack";
    public static string canMove = "canMove";
    public static string hasTarget = "hasTarget";
    public static string isAlive = "isAlive";

    public float CurrentMoveSpeed { get
        {
            if(CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {

                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }

                    }
                    else
                    {
                        return airWalkSpeed;
                    }

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
                
        }
    }

    private bool _isMoving = false;

    public bool IsMoving { get
        {
            return _isMoving; 
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight;  } private set { 
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value; 
        
        } }

    public bool CanMove { get
        {
            return animator.GetBool(canMove);
        } }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(isAlive);
        }
    }
    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed * Time.fixedDeltaTime, rb.velocity.y);

        animator.SetFloat(yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
       
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight= false;
        }
    }

    public void OnRun(InputAction.CallbackContext conext)
    {
        if (conext.started)
        {
            IsRunning = true;
        }
        else if (conext.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(attackTrigger);
        }
    }
}
