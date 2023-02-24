using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

public class sActorAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private S_ActorController _sActorController;
    private readonly int _death = Animator.StringToHash("Death");
    private readonly int _deathGibbed = Animator.StringToHash("DeathGibbed");

    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _isWalking = Animator.StringToHash("IsWalking");

    [SerializeField] private GameObject _bloodFX;
    
    

    private void Start() {
        _animator = GetComponent<Animator>();
        _sActorController = GetComponentInParent<S_ActorController>();
        _sActorController.ActorEventManager.AddListener<DamageTakenEvent>(OnDamageTaken);
        _sActorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
        _sActorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);
        _sActorController.ActorEventManager.AddListener<StartWalkingEvent>(OnStartWalking);


    }

    private void OnDestroy() {
        _sActorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnDamageTaken);
        _sActorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
        _sActorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);
        _sActorController.ActorEventManager.RemoveListener<StartWalkingEvent>(OnStartWalking);

    }

    

    private void OnDamageTaken(DamageTakenEvent obj) {
        Instantiate(_bloodFX, obj.Hit.point, Quaternion.identity, transform);
    }

    private void OnDeath(ActorDeathEvent obj) {
        if (obj.Gibbed) {
            _animator.SetTrigger(_deathGibbed);
        }
        else {
            _animator.SetTrigger(_death);
        }
        Instantiate(_bloodFX, obj.Hit.point, Quaternion.identity, transform);
    }
    
    private void OnAttack(ActorAttackEvent obj) {
        _animator.SetTrigger(_attack);
        _animator.SetBool(_isWalking, false);

    }
    
    private void OnStartWalking(StartWalkingEvent obj) {
        _animator.SetBool(_isWalking, true);
    }

    /*public void Move() {
    }*/
}
