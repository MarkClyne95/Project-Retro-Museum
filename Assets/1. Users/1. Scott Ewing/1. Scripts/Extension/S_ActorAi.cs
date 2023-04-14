using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using SensorToolkit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectRetroMuseum.ScottEwing{
    public class S_ActorAi : MonoBehaviour{
        private S_ActorController _actorController;
        //[SerializeField] private Transform target;
        //private sActorAnimator _actorAnimator;
        private NavMeshAgent _agent;


        enum ActorStates{
            Idle, Walking, Attacking, Dead
        }

        private ActorStates _actorState = ActorStates.Idle;
        public void Start() {
            _actorController = GetComponentInParent<S_ActorController>();
            //_target = GameObject.FindWithTag("Player").transform;
            //_actorAnimator = _actorController.GetComponentInChildren<sActorAnimator>();
            _agent =_actorController.GetComponent<NavMeshAgent>();
            
            _actorController.ActorEventManager.AddListener<DamageTakenEvent>(OnDamageTaken);
            _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
            _actorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);

        }
        
        public void OnDestroy() {
            _actorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnDamageTaken);
            _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
            _actorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);
        }

        public void Update() {
            if (!_agent.enabled) {
                return;
            }
            
            UpdateState();

            switch (_actorState) {
                case ActorStates.Idle:
                    _agent.isStopped = true;
                    break;
                case ActorStates.Walking:
                    _agent.isStopped = false;
                    _agent.SetDestination(_actorController.Target.position);
                    break;
                case ActorStates.Attacking:
                    _agent.isStopped = true;
                    break;
                /*case ActorStates.Dead:
                    if (_agent.enabled) {
                        _agent.isStopped = true;
                    }
                    break;*/
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void UpdateState() {
            var target = _actorController.Target;
            
            if (_actorState == ActorStates.Dead) {
                return;
            }else if (target == null) {
                _actorState = ActorStates.Idle;
            }
        
            /*if (_actorController._actorAttack.CanAttack(out _, out _)) {
                _actorState = ActorStates.Attacking;
            }*/
            
            else if (target != null && !_actorController._actorAttack.IsAttacking && _actorState != ActorStates.Walking) {
                _actorState = ActorStates.Walking;
                _actorController.BroadCastStartWalkingEvent(target);
            }
        
        }

        public void TargetFound(GameObject target, Sensor sensor) {
            //this.target = target.transform;
            _actorController.Target = target.transform;
        }
        
        private void OnAttack(ActorAttackEvent obj) {
            _actorState = ActorStates.Attacking;
        }

        private void OnDeath(ActorDeathEvent obj) {
            _actorState = ActorStates.Dead;
            _agent.enabled = false;
        }

        private void OnDamageTaken(DamageTakenEvent obj) {
        }
        
        
        
    }
    
    /*[Serializable]
    public class S_ActorAi : IActorComponent{
        private S_ActorController _actorController;
        [SerializeField] private Transform _target;
        //private sActorAnimator _actorAnimator;
        private NavMeshAgent _agent;


        enum ActorStates{
            Idle, Walking, Attacking, Dead
        }

        private ActorStates _actorState = ActorStates.Idle;
        public void Initialise(S_ActorController sActorController) {
            _actorController = sActorController;
            _target = GameObject.FindWithTag("Player").transform;
            //_actorAnimator = _actorController.GetComponentInChildren<sActorAnimator>();
            _agent =_actorController.GetComponent<NavMeshAgent>();
            
            _actorController.ActorEventManager.AddListener<DamageTakenEvent>(OnDamageTaken);
            _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
            _actorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);

        }

        

        public void OnDestroy() {
            _actorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnDamageTaken);
            _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
            _actorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);
        }

        public void Update() {
            UpdateState();

            switch (_actorState) {
                case ActorStates.Idle:
                    _agent.isStopped = true;
                    break;
                case ActorStates.Walking:
                    _agent.isStopped = false;
                    _agent.SetDestination(_target.position);
                    break;
                case ActorStates.Attacking:
                    _agent.isStopped = true;
                    break;
                case ActorStates.Dead:
                    _agent.isStopped = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void UpdateState() {
            if (_actorState == ActorStates.Dead) {
                return;
            }else if (_target == null) {
                _actorState = ActorStates.Idle;
            }
        
            if (_actorController._actorAttack.CanAttack(out _, out _)) {
                _actorState = ActorStates.Attacking;
            }else if (!_actorController._actorAttack.IsAttacking) {
                _actorState = ActorStates.Walking;
            }
        
        }
        
        private void OnAttack(ActorAttackEvent obj) {
            _actorState = ActorStates.Attacking;
        }

        private void OnDeath(ActorDeathEvent obj) {
            _actorState = ActorStates.Dead;
        }

        private void OnDamageTaken(DamageTakenEvent obj) {
        }
        
    }*/
}
