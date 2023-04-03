using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class S_MetroidVaniaPlayerController : S_Character, PlayerInputs.IPlayerActions
{
    //Public properties
    [Header("Locomotion")] public float movementSpeed;
    public float jumpHeight;
    private int jumpCount = 1;
    [SerializeField]private bool _onLadder;
    public Vector2 _moveInput;
    public bool canMove;
    public bool isJumping;
    public bool isGrounded;
    private bool _isLookingLeft;
    private float localScaleForY;
    public RenderPipelineAsset pipelineAsset;

    //Private Properties
    private bool _falling;
    private float _maxFallSpeed;
    private float _checkRadius;
    private float _jumpForce;

    //animator hash int
    private int horizontal;
    private int vertical;
    private int jump;

    //Ground Detection
    [Header("Ground Detection")] [SerializeField]
    private Transform collisionGround;

    [SerializeField] private Vector3 collisionGroundPos;

    //Game Objects
    [Header("Game Objects")]
    [SerializeField] private GameObject feet;
    [SerializeField] private LayerMask whatIsGround;
    public float checkRadius;
    private PlayerInputs _playerInputs;
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer sr;
    [SerializeField]private S_GameManager _gm;

    #region Singleton

    public static S_MetroidVaniaPlayerController instance;

    #endregion


    #region Mono methods

    private void Awake()
    {
        instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _playerInputs = new PlayerInputs();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _playerInputs.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Player.Disable();
    }

    private void FixedUpdate()
    {
        Flip();
        isGrounded = Physics2D.OverlapCircle(collisionGround.position, checkRadius, whatIsGround);

        _rb.velocity = new Vector2(_moveInput.x * movementSpeed, _rb.velocity.y);
    }

    private void Update()
    {
        
    }

    private void Start()
    {
        QualitySettings.SetQualityLevel(5, true);
        _anim = GetComponent<Animator>();
        canMove = true;
        localScaleForY = transform.localScale.y;
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        jump = Animator.StringToHash("Jump");
    }

    public void OnMovement(InputAction.CallbackContext input)
    {
        _moveInput = input.ReadValue<Vector2>();
        if (_moveInput.x < 0 && !_onLadder)
        {
            _isLookingLeft = true;
        }
        else if (_moveInput.x > 0 && !_onLadder)
        {
            _isLookingLeft = false;
        }
        
        _anim.SetBool("Run", _moveInput.x != 0);
    }

    private void Flip()
    {
        transform.localScale = _isLookingLeft ? new Vector2(-11, 11) : new Vector2(11, 11);
    }

    private void OnShoot(InputValue ctx)
    {
        if (ctx.isPressed)
        {
            Debug.LogError("Yeah");
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton() && isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
            _anim.SetBool(jump, true);
            Invoke(nameof(SetAnimBool), 0.5f);
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    private void SetAnimBool()
    {
        _anim.SetBool(jump, false);
    }

    #endregion
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (collisionGround.position , checkRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            _onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            _onLadder = false;
        }
    }
}