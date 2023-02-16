using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

public class S_ActorController : MonoBehaviour{
    private ActorEventManager _actorEventManager = new ActorEventManager();


    public void TakeDamageEvent(int damage, int currentHealth) {
        var evt = new DamageTakenEvent() {
            DamageTaken = damage,
            RemainingHealth = currentHealth
        };
        _actorEventManager.Broadcast(evt);
    }

    public void DeathEvent(int damage, int currentHealth) {
        var evt = new ActorDeathEvent() {
            DamageTaken = damage,
        };
        _actorEventManager.Broadcast(evt);
    }
}
