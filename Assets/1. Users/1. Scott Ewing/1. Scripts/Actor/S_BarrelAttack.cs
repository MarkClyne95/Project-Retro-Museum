using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using SensorToolkit;
using UnityEngine;

public class S_BarrelAttack : S_ActorAttack{
    [SerializeField] private RangeSensor _rangeSensor;

    protected override void Start() {
        base.Start();
        _rangeSensor = GetComponent<RangeSensor>();
        _actorController.ActorEventManager.AddListener<TryAttackEvent>(OnTryAttack);
    }

    protected override void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<TryAttackEvent>(OnTryAttack);
        base.OnDestroy();
    }

    private void OnTryAttack(TryAttackEvent obj) {
        TryAttack();
    }

    public override bool TryAttack() {
        if (!CanAttack(out var damageTakers, out var hits)) return false;
        Attack(damageTakers, hits);
        return true;
    }

    public bool CanAttack(out List<ITakesDamage> damageTakerList, out List<RaycastHit> hits) {
        damageTakerList = new List<ITakesDamage>();
        hits = new List<RaycastHit>();
        if (!_attackCooldownOver) {
            return false;
        }

        var detectedObjects = _rangeSensor.GetDetected();

        foreach (var detectedObject in detectedObjects) {
            if (detectedObject.TryGetComponent<ITakesDamage>(out var takesDamage)) {
                damageTakerList.Add(takesDamage);
                hits.Add(new RaycastHit());
            }
        }
        return true;
    }

    protected void Attack(List<ITakesDamage> damageTakerList, List<RaycastHit> hits) {
        //_actorController.BroadcastAttackedEvent();
        _attackCooldownOver = false;
        IsAttacking = true;
        foreach (var takesDamage in damageTakerList) {
            takesDamage.TakeDamage(_damage, new RaycastHit());
        }
    }

    protected override void OnActorDeath(ActorDeathEvent obj) {
        //TryAttack();
    }
}