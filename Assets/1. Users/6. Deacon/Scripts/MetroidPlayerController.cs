using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Globalization;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class MetroidPlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 6f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public GameObject fallDetector2;
    public TextMeshProUGUI scoreText;


    public GameObject interactNotification;

    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;


    static string yVelocity = "yVelocity";
    static string jumpTrigger = "Jump";
    static string attackTrigger = "attack";
    public static string canMove = "canMove";
    public static string hasTarget = "hasTarget";
    public static string isAlive = "isAlive";
    public static string hitTrigger = "hit";
    public static string lockVelocity = "lockVelocity";
    public static string attackCooldown = "attackCooldown";

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        fallDetector2.transform.position = new Vector2(transform.position.x, fallDetector2.transform.position.y);
    }
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
    TrailRenderer tr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        respawnPoint = transform.position;
        scoreText.text = "Score : 00" + MetroidScoring.totalScore;

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if(!damageable.LockVelocity)
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

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void NotifyPlayer()
    {
        interactNotification.SetActive(true);
    }

    public void DeNotifyPlayer()
    {
        interactNotification.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDectector")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "CheckPoint")
        {
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Dia")
        {
            MetroidScoring.totalScore += 1000;
            scoreText.text = "Score : 00" + MetroidScoring.totalScore;
            collision.gameObject.SetActive(false);
        }
    }

}
