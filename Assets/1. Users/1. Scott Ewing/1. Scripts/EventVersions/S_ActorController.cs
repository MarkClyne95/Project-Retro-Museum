using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using Sirenix.OdinInspector.Editor.StateUpdaters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class S_ActorController : MonoBehaviour{
    public ActorEventManager ActorEventManager = new ActorEventManager();
    [SerializeField] private s_ActorAudio _actorAudio;
    [SerializeField] public S_ActorAttack _actorAttack;
    private sActorAnimator _actorAnimator;

    public Transform Target { get; set; }
    //[SerializeField] private S_Health _health;
    //--Movement
    //private NavMeshAgent _agent;
    //private Transform _target;
    private bool _canMove;

    /*enum ActorStates{
        Idle, Walking, Attacking, Dead
    }

    private ActorStates _actorState = ActorStates.Idle;*/
    
    protected virtual void Start() {
        _actorAudio.Initialise(this);
        _actorAnimator = GetComponentInChildren<sActorAnimator>();
        _actorAttack = GetComponentInChildren<S_ActorAttack>();
    }

    private void OnDestroy() {
        _actorAudio.OnDestroy();
        //_actorAttack.OnDestroy();

    }

    /*private void Update() {
        //if can see player
        var attacked =_actorAttack.TryAttack(_target.position); 
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

    public void UpdateState() {
        if (_actorState == ActorStates.Dead) {
            return;
        }else if (_target == null) {
            _actorState = ActorStates.Idle;
        }
        
        if (_actorAttack.CanAttack(out _, out _)) {
            _actorState = ActorStates.Attacking;
        }else if (!_actorAttack.IsAttacking) {
            _actorState = ActorStates.Walking;
        }
        
    }*/

    
    

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
        //var evt = new ActorAttackEvent();
        ActorEventManager.Broadcast(Events.ActorAttackEvent);
    }

    public void BroadCastStartWalkingEvent(Transform target) {
        Events.StartWalkingEvent.Target = target;
        ActorEventManager.Broadcast(Events.StartWalkingEvent);
    }
}
