using System;
using System.Collections;
using SensorToolkit.Example;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class S_MetroidVaniaPlayerController : S_Character, PlayerInputs.IPlayerActions
{
    //Public properties
    [Header("Locomotion")] 
    public float movementSpeed;
    public float jumpHeight;
    public float maxJumpHeight;
    private int jumpCount = 1;
    [SerializeField]private bool _onLadder;
    public Vector2 _moveInput;
    public bool canMove;
    public bool isJumping;
    public bool isGrounded;
    public bool canShoot;
    public bool _isLookingLeft;
    public bool _invincible;

    [Header("Bullet Properies")] 
    [SerializeField] private Transform bulletStart;

    //Private Properties
    private bool _falling;
    private float _maxFallSpeed;
    private float _checkRadius;
    private float _jumpForce;

    //animator hash int
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
    [SerializeField] private GameObject _bullet;
    public float bulletSpeed;
    public Image healthBar;
    [SerializeField] private GameObject pauseUI;

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
        isGrounded = Physics2D.OverlapCircle(collisionGround.position, checkRadius, whatIsGround);

        if (!isGrounded && !_onLadder)
        {
            _anim.SetBool("Jump", true);
        }
        else
        {
            _anim.SetBool("Jump", false);
        }

        _rb.velocity = new Vector2(_moveInput.x * movementSpeed, _rb.velocity.y);
    }

    private void Update()
    {
        
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        canMove = true;
        _isLookingLeft = false;
        jump = Animator.StringToHash("Jump");
        healthBar.fillAmount = 1;
    }

    public void OnMovement(InputAction.CallbackContext input)
    {
        _moveInput = input.ReadValue<Vector2>();
        Debug.Log(_moveInput);
        if (_moveInput.x < 0 && !_onLadder)
        {
            _isLookingLeft = true;
            Flip();
        }
        else if (_moveInput.x > 0 && !_onLadder)
        {
            _isLookingLeft = false;
            Flip();
        }
        
        _anim.SetBool("Run", _moveInput.x != 0);
    }

    public void OnPause(InputAction.CallbackContext input)
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        
    }

    private void Flip()
    {
        transform.localScale = _isLookingLeft ? new Vector2(-11, 11) : new Vector2(11, 11);
    }

    public void SetHitpoints(float amount)
    {
        healthPoints -= amount;
    }

    public float GetHitpoints()
    {
        return healthPoints;
    }

    public void TakeDamage(float amount)
    {
        if (!_invincible)
        {
            _invincible = true;
            healthPoints -= amount;
            healthBar.fillAmount = healthPoints / maxHealthPoints;
            Mathf.Clamp(healthPoints, 0, maxHealthPoints);

            if (healthPoints <= 0)
            {
                Respawn();
            }
            _invincible = false;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (_anim.GetBool("Run") == true)
            {
                Invoke(nameof(ShootBullet), 0.1f);
                _anim.SetTrigger("RunShoot");
            }
            else
            {
                Invoke(nameof(ShootBullet), 0.1f);
                _anim.SetTrigger("Shoot");
            }
        }
    }

    private void ShootBullet()
    {
        var bul = Instantiate(_bullet, bulletStart.position, Quaternion.identity);
    }
    
    public void OnRun(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton() && isGrounded)
        {
            isJumping = true;
            _rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
    #endregion
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (collisionGround.position , checkRadius);
    }

    public void Respawn()
    {
        transform.position = new Vector2(501, 309);
        healthPoints = maxHealthPoints;
        healthBar.fillAmount = 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            _onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            _onLadder = false;
        }
    }
}