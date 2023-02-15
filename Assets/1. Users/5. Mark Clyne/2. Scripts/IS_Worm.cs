using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_Worm : S_Enemy
{
    [Header("Patrol Points")] 
    private Vector3 _target;
    private Vector3 _velocity;
    private Vector3 _previousPosition;
    [SerializeField] protected Transform[] waypoints;
    [SerializeField] private float timeBetweenPatrols;
    public float movementSpeed;
    [SerializeField] private bool isFacingRight;

    private bool _flipped;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(enemyType != EnemyDefinition.STATIC) _target = waypoints[1].position;
    }

    private void Start()
    {
        //spriteRenderer.flipX = isFacingRight;
        currentState = EnemyStates.PATROL;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyStates.IDLE:
                break;
            
            case EnemyStates.PATROL:
                Patrol();
                break;
            
            case EnemyStates.CHASE:
                Chase();
                break;
            
            case EnemyStates.ATTACK:
                AttackTrigger();
                break;
            
            case EnemyStates.DEAD:
                Death();
                break;
        }
        
        CheckLineOfSight();
    }

    private void AttackTrigger()
    {
        anim.SetBool("Move", false);  
        anim.SetBool("Idle", true);
    }

    protected override void Death()
    {
        anim.SetBool("Dead", true);
        health.isDead = true;
    }
    
    public override void Attack()
    {
        if (canAttack && !health.isHit && !health.isDead)
        {
            player.GetComponent<Health>().invincible = false;
            anim.SetTrigger("AttackTrigger");
            canAttack = false;
            attackTimer = attackCooldown;
        }
    }
    
    public virtual void Patrol()
    {
        if (Math.Abs(transform.position.x - _target.x) > 0.01)
        {
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
            transform.position = Vector3.MoveTowards(transform.position, _target, movementSpeed * Time.deltaTime);
        }

        else
        {
            if (Math.Abs(_target.x - waypoints[0].position.x) < 0.01)
            {
                _target = waypoints[1].position;
                if (_flipped)
                {
                    _flipped = !_flipped;
                    StartCoroutine(SetWalkTarget(waypoints[1].position));
                }
            }
            else
            {
                _target = waypoints[0].position;
                if (!_flipped)
                {
                    _flipped = !_flipped;
                    StartCoroutine(SetWalkTarget(waypoints[0].position));
                }
            }
        }
    }

    protected virtual void Chase()
    {
        _target = player.position;
        if (Math.Abs(transform.position.x - _target.x) > 0.1f)
        {
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
            transform.position = Vector3.MoveTowards(transform.position, _target, movementSpeed * Time.deltaTime);
            FaceTowards(player.position - transform.position);
        }

        else
        {
            if (Math.Abs(_target.x - player.position.x) < 0.1f)
            {
                if (_flipped)
                {
                    _flipped = !_flipped;
                    SetChaseTarget(player.position);
                }
            }
            else
            {
                if (!_flipped)
                {
                    _flipped = !_flipped;
                    SetChaseTarget(player.position);
                }
            }
        }
    }

    protected virtual IEnumerator SetWalkTarget(Vector3 pos)
    {
        anim.SetBool("Move", false);
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(timeBetweenPatrols);
        _target = pos;
        FaceTowards(pos - transform.position);
    }

    protected virtual void SetChaseTarget(Vector3 pos)
    {
        FaceTowards(pos - transform.position);
        _target = pos;
    }
}