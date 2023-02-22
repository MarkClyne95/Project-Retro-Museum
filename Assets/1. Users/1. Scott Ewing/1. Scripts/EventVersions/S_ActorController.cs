using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class S_ActorController : MonoBehaviour{
    public ActorEventManager ActorEventManager = new ActorEventManager();
    [SerializeField] private s_ActorAudio _actorAudio;
    [SerializeField] private S_ActorAttack _actorAttack;
    private sActorAnimator _actorAnimator;
    //[SerializeField] private S_Health _health;
    //--Movement
    private NavMeshAgent _agent;
    private Transform _target;
    private bool _canMove;
    
    private void Start() {
        //_actorAudio = s_ActorAudio(this);
        _actorAudio.Initialise(this);
        _actorAttack.Initialise(this);
        //_health.Initialise(this);
        _agent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindWithTag("Player").transform;
        _actorAnimator = GetComponentInChildren<sActorAnimator>();
    }

    private void OnDestroy() {
        _actorAudio.OnDestroy();
        _actorAttack.OnDestroy();

    }

    private void Update() {
        //if can see player
        var attacked =_actorAttack.TryAttack(); 
        if (attacked) {
            //stop moving
            _agent.ResetPath();
            _agent.isStopped = true;
        }
        else if (!_actorAttack.IsAttacking) {
            //Move towards player
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);
            _actorAnimator.Move();
        }
    }

    
    

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
    
    public void BroadcastAttackedEvent() {
        var evt = new ActorAttackEvent();
        ActorEventManager.Broadcast(evt);
    }
}
