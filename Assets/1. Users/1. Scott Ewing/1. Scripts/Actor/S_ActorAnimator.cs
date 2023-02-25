using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

public class S_ActorAnimator : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    protected S_ActorController _sActorController;
    protected readonly int _death = Animator.StringToHash("Death");
    protected readonly int _deathGibbed = Animator.StringToHash("DeathGibbed");

    protected readonly int _attack = Animator.StringToHash("Attack");
    //private readonly int _isWalking = Animator.StringToHash("IsWalking");

    [SerializeField] protected GameObject _bloodFX;
    
    

    protected virtual void Start() {
        _animator = GetComponent<Animator>();
        _sActorController = GetComponentInParent<S_ActorController>();
        _sActorController.ActorEventManager.AddListener<DamageTakenEvent>(OnDamageTaken);
        _sActorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
        _sActorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);
        //_sActorController.ActorEventManager.AddListener<StartWalkingEvent>(OnStartWalking);


    }

    protected virtual  void OnDestroy() {
        _sActorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnDamageTaken);
        _sActorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
        _sActorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);
        //_sActorController.ActorEventManager.RemoveListener<StartWalkingEvent>(OnStartWalking);

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
    
    protected virtual void OnAttack(ActorAttackEvent obj) {
        _animator.SetTrigger(_attack);
        //_animator.SetBool(_isWalking, false);

    }
    
    /*private void OnStartWalking(StartWalkingEvent obj) {
        _animator.SetBool(_isWalking, true);
    }*/

    /*public void Move() {
    }*/
}
