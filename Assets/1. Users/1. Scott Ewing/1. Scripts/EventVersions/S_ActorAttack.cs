using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class S_ActorAttack : IActorComponent
{
    
    private S_ActorController _sActorController;
    [SerializeField] private RayCastHelper _rayCastHelper;
    [SerializeField] private float _minAttackCooldown = 1f;
    [SerializeField] private float _maxAttackCooldown = 3f;
    
    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private bool _attackCooldownOver;
    [SerializeField] private int _damage = 10;

    [SerializeField] public float _attackDuration = 0.5f;
    [field: SerializeField] public bool IsAttacking { get; private set; }


    public void Initialise(S_ActorController sActorController) {
        _sActorController = sActorController;
        _sActorController.StartCoroutine(TempRoutine());
    }

    IEnumerator TempRoutine() {
        while (true) {
            yield return null;
            if (Input.GetKeyDown(KeyCode.P)) {
                if (CanAttack()) {
                    Attack();
                }
            }
        }
    }

    
    public void OnDestroy() {
        
    }

    public bool TryAttack() {
        if (!CanAttack()) return false;
        Attack();
        return true;
    }

    private bool CanAttack() {
        if (_attackCooldownOver) return false;
        /*if (_rayCastHelper.) {
            
        }*/
        return true;
    }
    private void Attack() {
        _sActorController.BroadcastAttackedEvent();
        _attackCooldownOver = true;
        IsAttacking = true;

        if (_rayCastHelper.RaycastForTargets(out var hit) && hit.collider.gameObject.TryGetComponent(out ITakesDamage damageTaker)) {
            damageTaker.TakeDamage(_damage, hit);
        }
        SetAttackCooldownTime();
        _sActorController.StartCoroutine(AttackTimerRoutine(_attackDuration, _attackCooldown));
    }

    private void SetAttackCooldownTime() => _attackCooldown = Random.Range(_minAttackCooldown, _maxAttackCooldown);

    private IEnumerator AttackTimerRoutine(float attackDuration, float cooldownDuration) {
        yield return new WaitForSeconds(attackDuration);
        IsAttacking = false;
        yield return new WaitForSeconds(cooldownDuration);
        _attackCooldownOver = false;

    }
}
