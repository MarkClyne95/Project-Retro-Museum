using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Damageable : MonoBehaviour
{

    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;

    Animator animator;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject exitButton;
    public GameObject door;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    { 
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if (_health <= 0)
            {
                IsAlive = false;
                if (gameObject.CompareTag("Player"))
                {
                    if (!IsAlive)
                    {
                        Cursor.visible = true;
                        gameOverText.SetActive(true);
                        restartButton.SetActive(true);
                        exitButton.SetActive(true);
                    }
                }
                if (gameObject.CompareTag("Boss"))
                {
                    if (!IsAlive)
                    {
                        door.SetActive(true);
                    }
                }
            }

        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvinvible;


    private float timeSinceHit;

    [SerializeField]
    private float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(MetroidPlayerController.isAlive, value);
            Debug.Log("IsAlive set" + value);
        }
    }

    public bool LockVelocity
    {
        get
        { return animator.GetBool(MetroidPlayerController.lockVelocity); }
        set
        {
            animator.SetBool(MetroidPlayerController.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInvinvible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvinvible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

    }


    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvinvible)
        {
            Health -= damage;
            isInvinvible = true;

            animator.SetTrigger(MetroidPlayerController.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }

        return false;
    }

    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;

            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }

    
 
}
