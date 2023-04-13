using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
//using Sirenix.OdinInspector.Editor.StateUpdaters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class S_ActorController : MonoBehaviour{
    public ActorEventManager ActorEventManager = new ActorEventManager();
    [HideInInspector] public S_ActorAttack _actorAttack;
    public Transform Target { get; set; }
    private bool _canMove;
    
    protected virtual void Start() => _actorAttack = GetComponentInChildren<S_HitscanActorAttack>();

    protected virtual void OnDestroy() { }

    public void BroadcastTakeDamageEvent(int damage, int currentHealth, RaycastHit raycastHit) {
        var evt = new DamageTakenEvent() {
            DamageTaken = damage,
            RemainingHealth = currentHealth,
            Hit = raycastHit
        };
        ActorEventManager.Broadcast(evt);
    }

    public void BroadcastDeathEvent(int damage, int currentHealth, RaycastHit raycastHit, bool isGibbed) {
        var evt = new ActorDeathEvent {
            DamageTaken = damage,
            Hit = raycastHit,
            Gibbed = isGibbed,
        };
        ActorEventManager.Broadcast(evt);
    }
    
    public void BroadcastAttackedEvent() => ActorEventManager.Broadcast(Events.ActorAttackEvent);

    public void BroadCastStartWalkingEvent(Transform target) {
        Events.StartWalkingEvent.Target = target;
        ActorEventManager.Broadcast(Events.StartWalkingEvent);
    }

    public void BroadcastTryAttack() => ActorEventManager.Broadcast(Events.TryAttackEvent);

    public void BroadcastReceiveHealth(int health) {
        Events.ReceiveHealthEvent.Health = health;
        ActorEventManager.Broadcast(Events.ReceiveHealthEvent);
    }
}
