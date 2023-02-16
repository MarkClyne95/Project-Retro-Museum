using System;
using System.Collections;
using ScottEwing.ExtensionMethods;
using UnityEngine;
using UnityEngine.Events;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

// The Trigger Type only works if triggered is called. Triggered is not called from this class
// It must be called from a derived class. This is why this was an abstract class.
// Will still keep non abstract but should be aware that trigger type will have no effect 
namespace ScottEwing.Triggers{
        /// <summary>
        /// States are anly valid for the frame they are received
        /// </summary>
        public enum TriggerState{
            Enter,
            Stay,
            Exit,
            None
        }
    public class Trigger : MonoBehaviour{
        private enum TriggeredType{
            DestroyOnTriggered,
            DisableOnTriggered,
            CooldownOnTriggered,
            RemainActive
        }
        
        private enum TriggeredBy{Tag,LayerMask}

        [SerializeField] private TriggeredBy _triggeredBy = TriggeredBy.Tag;
        [ShowIf("_triggeredBy", TriggeredBy.LayerMask)]
        [SerializeField] private LayerMask _triggeredByMask;

        [ShowIf("_triggeredBy", TriggeredBy.Tag)]
        [SerializeField] protected string _triggeredByTag = "Player";
        [SerializeField] private TriggeredType _triggeredType = TriggeredType.DestroyOnTriggered;
#if ODIN_INSPECTOR
        [ShowIf("_triggeredType", TriggeredType.CooldownOnTriggered)]
#endif
        [SerializeField] private float _cooldownTime = 2.0f;

        [field: SerializeField] protected bool IsActivatable { get; set; } = true;
        [SerializeField] protected bool isDebug;  
        [SerializeField] private bool _invokeOnTriggerExitWhenTriggered = false;
        [SerializeField] protected UnityEvent _onTriggered;
        [SerializeField] protected UnityEvent _onTriggerEnter;
        [SerializeField] protected UnityEvent _onTriggerStay;
        [SerializeField] protected UnityEvent _onTriggerExit;

        private Coroutine _cooldownRoutine;


        protected virtual void OnTriggerEnter(Collider other) {
            if (IsColliderValid(other)) {
                InvokeOnTriggerEnter();
            }
        }

        protected virtual void OnTriggerStay(Collider other) {
            if (IsColliderValid(other)) {
                InvokeOnTriggerStay();
            }
        }

        protected virtual void OnTriggerExit(Collider other) {
            if (IsColliderValid(other)) {
                InvokeOnTriggerExit();
            }
        }


        protected virtual TriggerState InvokeOnTriggerEnter() {
            if (isDebug) 
                Debug.Log("OnTriggerEnter", this);
            _onTriggerEnter?.Invoke();
            return TriggerState.Enter;
        }

        protected virtual TriggerState InvokeOnTriggerStay() {
            if (isDebug)
                Debug.Log("OnTriggerStay", this);       
            _onTriggerStay?.Invoke();
            return TriggerState.Stay;
        }

        protected virtual TriggerState InvokeOnTriggerExit() {
            if (isDebug)
                Debug.Log("OnTriggerExit", this);       
            _onTriggerExit?.Invoke();
            return TriggerState.Exit;
        }
        
        protected virtual bool Triggered() {
            if (!gameObject.activeSelf || !gameObject.activeInHierarchy) return false;
            if (!IsActivatable) return false;
            if (isDebug)
                Debug.Log("Triggered", this);       
            _onTriggered.Invoke();
            switch (_triggeredType) {
                case TriggeredType.DestroyOnTriggered:
                    Destroy(gameObject);
                    break;
                case TriggeredType.DisableOnTriggered:
                    DisableTriggerObject();
                    break;
                case TriggeredType.CooldownOnTriggered:
                    StartCooldown();
                    break;
            }

            if (_invokeOnTriggerExitWhenTriggered) {
                InvokeOnTriggerExit();
            }

            return true;
        }

        public void StartCooldown() {
            _cooldownRoutine = StartCoroutine(Cooldown());

            IEnumerator Cooldown() {
                IsActivatable = false;
                yield return new WaitForSeconds(_cooldownTime);
                _cooldownRoutine = null;
                IsActivatable = true;
            }
        }

        public void CancelCooldown() {
            if (_cooldownRoutine == null) return;
            StopCoroutine(_cooldownRoutine);
            IsActivatable = true;
        }


        public void DisableTriggerObject() {
            gameObject.SetActive(false);
        }

        /// Also checks if trigger is activatable
        protected bool IsColliderValid(Collider other) {
            if (!IsActivatable) {
                return false;
            }
            switch (_triggeredBy) {
                case TriggeredBy.Tag:
                    return other.CompareTag(_triggeredByTag);
                    
                case TriggeredBy.LayerMask:
                    return _triggeredByMask.IsLayerInLayerMask(other.gameObject.layer);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}