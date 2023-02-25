using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

public class S_MonsterAnimator : S_ActorAnimator
{
    //[SerializeField] private Animator _animator;
    //private S_ActorController _sActorController;
    //private readonly int _death = Animator.StringToHash("Death");
    //private readonly int _deathGibbed = Animator.StringToHash("DeathGibbed");

    //private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _isWalking = Animator.StringToHash("IsWalking");

    //[SerializeField] private GameObject _bloodFX;


    protected override void Start() {
        /*_animator = GetComponent<Animator>();
        _sActorController = GetComponentInParent<S_ActorController>();
        _sActorController.ActorEventManager.AddListener<DamageTakenEvent>(OnDamageTaken);
        _sActorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
        _sActorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);*/
        base.Start();
        _sActorController.ActorEventManager.AddListener<StartWalkingEvent>(OnStartWalking);


    }

    protected override void OnDestroy() {
        /*_sActorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnDamageTaken);
        _sActorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
        _sActorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);*/
        base.OnDestroy();
        _sActorController.ActorEventManager.RemoveListener<StartWalkingEvent>(OnStartWalking);

    }


    /*protected override void OnDamageTaken(DamageTakenEvent obj) {
        Instantiate(_bloodFX, obj.Hit.point, Quaternion.identity, transform);
    }*/

    /*protected override void OnDeath(ActorDeathEvent obj) {
        if (obj.Gibbed) {
            _animator.SetTrigger(_deathGibbed);
        }
        else {
            _animator.SetTrigger(_death);
        }
        Instantiate(_bloodFX, obj.Hit.point, Quaternion.identity, transform);
    }*/

    protected override void OnAttack(ActorAttackEvent obj) {
        _animator.SetTrigger(_attack);
        _animator.SetBool(_isWalking, false);

    }
    
    private void OnStartWalking(StartWalkingEvent obj) {
        _animator.SetBool(_isWalking, true);
    }

    /*public void Move() {
    }*/
}
