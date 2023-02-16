using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace ScottEwing.Triggers {
    public class ToggleInteractTrigger : InteractTrigger , ITakesInput{
        //[field: SerializeField] public InputActionProperty InputActionReference { get; set; }
        //public bool ShouldCheckInput { get; set; }
        [Tooltip("The Trigger has been toggled off")]
        [SerializeField] protected UnityEvent _onTriggeredOff;

        private bool _triggerOn;
        [SerializeField] private bool _turnOnOnFirstEnter;
        [Tooltip("Turns off the trigger on trigger exit only if trigger is currently on")]
        [SerializeField] private bool _turnOffOnTriggerExit = true;
        private bool _firstEnterComplete = false;
        //private void Update() => GetInput();

        protected override void OnTriggerEnter(Collider other) {
            if (!IsColliderValid(other)) return;
            //ShouldCheckInput = true;
            if (_turnOnOnFirstEnter && !_firstEnterComplete) {
                _firstEnterComplete = true;
                Triggered();
            }
            base.OnTriggerEnter(other);
        }

        protected override void OnTriggerExit(Collider other) {
            if (!IsColliderValid(other)) return;
            if (_turnOffOnTriggerExit && _triggerOn) {
                TriggerOff();
            }
            base.OnTriggerExit(other);
        }

        protected override bool Triggered() {
            _triggerOn = !_triggerOn;
            return _triggerOn ? base.Triggered() : TriggerOff();
        }

        public bool TriggerOff() {
            if (!gameObject.activeSelf || !gameObject.activeInHierarchy) return false;
            if (!IsActivatable) return false;
            if (isDebug)
                Debug.Log("Trigger Off", this);
            _triggerOn = false;
            _onTriggeredOff.Invoke();
            return true;
        }

        /*public void GetInput() {
            if (!IsActivatable) return;
            if (!ShouldCheckInput) return;
            if (InputActionReference.action == null) return;
            if (InputActionReference.action.triggered) {
                Triggered();
            }
        }
        protected override void OnTriggerEnter(Collider other) {
            if (!other.CompareTag(_triggeredByTag)) return;
            ShouldCheckInput = true;
            base.OnTriggerEnter(other);
        }

        protected override void OnTriggerExit(Collider other) {
            if (!other.CompareTag(_triggeredByTag)) return;
            ShouldCheckInput = false;
            base.OnTriggerExit(other);
        }*/
    } 
}
