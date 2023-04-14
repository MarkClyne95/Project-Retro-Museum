using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using SensorToolkit;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class S_HitscanActorAttack : S_ActorAttack{
    [SerializeField] private RaySensor _raySensor;

    protected override void Start() {
        _actorController = GetComponentInParent<S_ActorController>();
        _raySensor = _actorController.GetComponentInChildren<RaySensor>();
        _actorController.ActorEventManager.AddListener<StartWalkingEvent>(OnStartWalking);
        base.Start();
    }

    protected override void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<StartWalkingEvent>(OnStartWalking);
        base.OnDestroy();
    }

    public override bool TryAttack() {
        if (!CanAttack(out var damageTaker, out var hit)) return false;
        Attack(damageTaker, hit);
        return true;
    }

    public override bool CanAttack(out ITakesDamage damageTaker, out RaycastHit hit) {
        damageTaker = null;
        hit = new RaycastHit();
        if (!_attackCooldownOver) return false;
        var detectedObjects = _raySensor.GetDetected();
        if (detectedObjects.Count > 0 && detectedObjects[0].TryGetComponent(out damageTaker)) {
            if ((_requireTarget && _raySensor.IsDetected(_actorController.Target.gameObject) || !_requireTarget)) {
                hit = _raySensor.GetRayHit(detectedObjects[0]);
                return true;
            }
        }
        else if (detectedObjects.Count == 0 && !_requireTarget) {
            if (_raySensor.IsObstructed) {
                hit = _raySensor.ObstructionRayHit;
                return true;
            }
        }
        return false;
    }

    protected override void Attack(ITakesDamage damageTaker, RaycastHit hit) {
        _actorController.BroadcastAttackedEvent();
        _attackCooldownOver = false;
        IsAttacking = true;
        if (damageTaker != null && damageTaker.TakeDamage(_damage, hit)) {
            _actorController.Target = null;
            if (_attackRoutine != null) {
                StopCoroutine(_attackRoutine);
            }
        }
        SetAttackCooldownTime();
        _attackTimerRoutine = StartCoroutine(AttackTimerRoutine());
    }

    private void OnStartWalking(StartWalkingEvent obj) {
        _actorController.Target = obj.Target;
        if (_attackRoutine != null) {
            return;
        }
        _attackRoutine = StartCoroutine(AttackRoutine());
    }
}