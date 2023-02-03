using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK,
    DEAD
}

public enum EnemyDefinition
{
    STATIC,
    PATROLLING,
    CHASER,
}

public class S_Enemy : MonoBehaviour
{

    [Header("Sprite Details and Movement Components")]
    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;
    public Animator anim;

    [Header("Base Stats")] public Health health;
    public Damager damager;
    public bool flyingEnemy;

    [Header("Current State and Enemy Type")]
    public EnemyStates currentState;

    public EnemyDefinition enemyType;

    [Header("Attack Components and Variables")]
    public bool canAttack;

    public float attackCooldown;
    public float attackTimer;

    [Header("Colliders and Checkers")] [SerializeField]
    private Transform wallCheck;

    [SerializeField] private Transform dropCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundDefinintion;
    private bool _isGrounded;

    [Header("Player Detection and Line of Sight")]
    public float lineOfSightRadius;

    public float lineOfSightDistance;
    public Transform player;
    public bool seenPlayer;
    public float distanceToPlayer;
    public LayerMask playerLayer;

    private RaycastHit _hit;

    public virtual void Update()
    {
        if (health.isDead)
        {
            currentState = EnemyStates.DEAD;
        }
    }

    protected virtual void CheckLineOfSight()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, lineOfSightRadius, playerLayer);

        if (colliders.Length > 0)
        {
            foreach (var i in colliders)
            {
                distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (i.CompareTag("Player") && (distanceToPlayer > 0.8f) && enemyType != EnemyDefinition.STATIC)
                {
                    currentState = EnemyStates.CHASE;
                    seenPlayer = true;
                }
                else if (i.CompareTag("Player") && (distanceToPlayer <= 0.5f))
                {
                    currentState = EnemyStates.ATTACK;
                    seenPlayer = true;
                }
            }
        }
        else
        {
            currentState = EnemyStates.PATROL;
            seenPlayer = false;
        }
    }

    protected virtual void Death()
    {

    }

    public virtual void Attack()
    {
    }

    protected virtual void FaceTowards(Vector3 dir)
    {
        transform.eulerAngles = dir.x < 0f ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = seenPlayer ? Color.red : Color.green;

        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawSphere(new Vector3(0f, 0f, lineOfSightDistance / 2f), lineOfSightRadius);
    }
}

public class Health
{
    public bool isDead;
    public bool isHit;
    public bool invincible;
}

public class Damager
{
    
}
