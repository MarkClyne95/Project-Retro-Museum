using System.Collections;
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
    public float runSpeed = 1;
    [SerializeField] private Vector2 _moveInput;
    public bool canMove;
    public bool isRunning;
    public bool isJumping;
    public bool isGrounded;
    public bool movingOnStairs;
    private bool _isLookingLeft;
    private float localScaleForY;
    public RenderPipelineAsset pipelineAsset;

    //Private Properties
    private bool _canDoubleJump;
    private bool _doubleJumped;
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
    [Header("Game Objects")] [SerializeField]
    private GameObject dustEffect;

    [SerializeField] private GameObject feet;
    //[SerializeField] private GameObject wings;
    [SerializeField] private LayerMask whatIsGround;
    public float checkRadius;
    //private PlayerState _playerState;
    private PlayerInputs _playerInputs;
    private Rigidbody2D _rb;
    //private Health _health;
    private Animator _anim;
    //private PlayerAttacks _playerAttacks;
    //private SoundManager _sound;
    private SpriteRenderer sr;
    
    [Tooltip("Angel Guide GameObject")] 
    public GameObject angelGuide;

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
        if (isGrounded)
        {
            if (isJumping)
            {
                _doubleJumped = false;
                isJumping = false;
                //_anim.SetBool("Jump", false);
            }
            else
            {
                _anim.SetBool("Jump", false);
                //_anim.SetLayerWeight(1, 1);
            }
        }
        else
        {
            _anim.SetBool("Jump", true);
            //_anim.SetLayerWeight(1, 0);
        }
    }

    private void Start()
    {
        QualitySettings.SetQualityLevel(5, true);
        _anim = GetComponent<Animator>();
        //_health = GetComponent<Health>();
        //_playerAttacks = GetComponent<PlayerAttacks>();
        //wings.SetActive(false);
        canMove = true;
        //_sound = SoundManager.instance;
        localScaleForY = transform.localScale.y;

        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        jump = Animator.StringToHash("Jump");
    }

    public void OnPreviousFeat(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMovement(InputAction.CallbackContext input)
    {
        _moveInput = input.ReadValue<Vector2>();
        // if (isGrounded) _jumpForce = 0;
        //
        if (_moveInput.x < 0)
        {
            _isLookingLeft = true;
        }
        else if (_moveInput.x > 0)
        {
            _isLookingLeft = false;
        }
        _anim.SetBool("Run", _moveInput.x != 0);
    }

    private void Flip()
    {
        transform.localScale = _isLookingLeft ? new Vector2(-1, 1) : new Vector2(1, 1);
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

    public void OnHammerAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnThrowHammer(InputAction.CallbackContext context)
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
            isGrounded = false;
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

    public void OnNextFeat(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    #endregion
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (collisionGround.position , checkRadius);
    }
}