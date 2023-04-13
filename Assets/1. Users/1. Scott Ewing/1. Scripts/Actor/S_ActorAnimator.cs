using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

public class S_ActorAnimator : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    protected S_ActorController _actorController;
    protected readonly int _death = Animator.StringToHash("Death");
    protected readonly int _deathGibbed = Animator.StringToHash("DeathGibbed");
    protected readonly int _attack = Animator.StringToHash("Attack");
    [SerializeField] protected GameObject _bloodFX;
    
    protected virtual void Start() {
        _animator = GetComponent<Animator>();
        _actorController = GetComponentInParent<S_ActorController>();
        _actorController.ActorEventManager.AddListener<DamageTakenEvent>(OnDamageTaken);
        _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
        _actorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);
    }

    protected virtual  void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnDamageTaken);
        _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
        _actorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);
    }

    protected virtual  void OnDamageTaken(DamageTakenEvent obj) {
        if (_bloodFX != null) {
            Instantiate(_bloodFX, obj.Hit.point, Quaternion.identity, transform);
        }
    }

    protected virtual  void OnDeath(ActorDeathEvent obj) {
        if (obj.Gibbed) {
            _animator.SetTrigger(_deathGibbed);
        }
        else {
            _animator.SetTrigger(_death);
        }
        if (_bloodFX != null) {
            Instantiate(_bloodFX, obj.Hit.point, Quaternion.identity, transform);
        }
    }
    
    protected virtual void OnAttack(ActorAttackEvent obj) => _animator.SetTrigger(_attack);
}
