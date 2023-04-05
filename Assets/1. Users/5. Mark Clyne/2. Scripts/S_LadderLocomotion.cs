using System;
using UnityEngine;

public class S_LadderLocomotion : MonoBehaviour
{
    #region Properties
    private bool _isLadder;
    private bool _isClimbing;
    private float _vertical;
    private Rigidbody2D _rb;
    private S_MetroidVaniaPlayerController _playerController;
    [SerializeField]private float _speed;
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<S_MetroidVaniaPlayerController>();
    }

    private void Update()
    {
        _vertical = _playerController._moveInput.y;

        if (_isLadder && Math.Abs(_vertical) > 0.0f)
        {
            _isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isClimbing)
        {
            _rb.gravityScale = 0;
            _rb.velocity = new Vector2(_rb.velocity.x, _vertical * _speed);
        }
        else
        {
            _rb.gravityScale = 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            _isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            _isLadder = false;
            _isClimbing = false;
        }
    }
}
