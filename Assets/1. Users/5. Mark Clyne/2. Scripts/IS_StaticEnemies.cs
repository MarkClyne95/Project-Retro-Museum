using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_StaticEnemies : S_Enemy
{
    [Header("Static Enemy Specific Line Of Sight")] 
    [SerializeField] private float aggroDistance;

    [Header("Static Enemy Specific Attack Variables")] 
    [SerializeField] private GameObject projectileL;
    [SerializeField] private GameObject projectileR;
    public Vector3 projectileSpawnPosL;
    public Vector3 projectileSpawnPosR;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //damager = GetComponentInChildren<Damager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //health = GetComponent<Health>();
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
                //Patrol();
                break;
            
            case EnemyStates.CHASE:
                //Chase();
                break;
            
            case EnemyStates.ATTACK:
                AttackTrigger();
                break;
            
            case EnemyStates.DEAD:
                break;
        }
        
        CheckLineOfSight();
    }

    protected override void CheckLineOfSight()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, lineOfSightRadius, playerLayer);

        if (colliders.Length > 0)
        {
            foreach (var i in colliders)
            {
                distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                FaceTowards(transform.position - player.transform.position);
                if (i.CompareTag("Player") && (distanceToPlayer < aggroDistance))
                {
                    currentState = EnemyStates.ATTACK;
                    seenPlayer = true;
                }
            }
        }
    }
    
    private void AttackTrigger()
    {
        if (!canAttack && !health.isHit)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                canAttack = true;
                Attack();
            }
        }
    }

    public override void Attack()
    {
        if (canAttack && !health.isHit && !health.isDead)
        {
            player.GetComponent<Health>().invincible = false;
            anim.SetTrigger("Throw");
            canAttack = false;
            attackTimer = attackCooldown;
        }
    }
}
