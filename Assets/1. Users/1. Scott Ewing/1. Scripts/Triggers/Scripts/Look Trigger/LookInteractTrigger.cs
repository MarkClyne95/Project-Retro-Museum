using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScottEwing.Triggers{
    public class LookInteractTrigger : Trigger, ILookInteractable, ITakesInput{
        [field:SerializeField] public InputActionProperty InputActionReference { get; set; }
        [SerializeField] protected float _maxInteractDistance = 1;

        public bool ShouldCheckInput { get; set; }

        private bool _isLooking;
        [Description("Used to detect the first frame that this trigger was not looked at")]
        private bool _lookedThisFixedUpdate;

        #region Unity Methods

        private void Update() => GetInput();
        protected virtual void FixedUpdate() => HandleFirstUpdateNotLookedAt();
        //-- Dont want base behaviour
        protected override void OnTriggerEnter(Collider other) { }
        protected override void OnTriggerStay(Collider other){ }
        protected override void OnTriggerExit(Collider other){ }

        #endregion
        
        
        /// <summary>
        /// Get Update runs every frame that this trigger is looked at 
        /// </summary>
        public void GetInput() {
            if (!IsActivatable) return;
            if (!ShouldCheckInput) return;
            if (InputActionReference.action == null) return;
            if (InputActionReference.action.triggered) {
                Triggered();
            }
        }

        /// <summary>
        /// Determines if this fixed update was the first time the trigger was not looked and calls look exit if it was
        /// </summary>
        private void HandleFirstUpdateNotLookedAt() {
            if (!_isLooking && _lookedThisFixedUpdate) {
                _isLooking = true;
            }
            if (_isLooking && !_lookedThisFixedUpdate) {
                _isLooking = false;
                InvokeOnTriggerExit();
            }
            _lookedThisFixedUpdate = false;     // reset this for next frame
        }

        public TriggerState Look(Vector3 cameraPosition) {
            _lookedThisFixedUpdate = true;
            if (!IsActivatable) return TriggerState.None;
            if (!ShouldCheckInput && CanCameraActivateTrigger(cameraPosition)) {
                return InvokeOnTriggerEnter();
            }
            else if (ShouldCheckInput && CanCameraActivateTrigger(cameraPosition)) {
                return InvokeOnTriggerStay();
            }
            else if (ShouldCheckInput && !CanCameraActivateTrigger(cameraPosition)) {
                return InvokeOnTriggerExit();   // camera got too far away
            }
            return TriggerState.None;
        }
        
        // should work trigger this but untested
        /*public virtual bool TryTrigger(TriggerState state) {
            switch (state) {
                case TriggerState.Enter:
                case TriggerState.Stay:
                    if (!ShouldCheckInput) return false;
                    Triggered();
                    return true;
                case TriggerState.Exit:
                case TriggerState.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }*/

        protected override TriggerState InvokeOnTriggerEnter(Collider other = null) {
            if (ShouldCheckInput) {
                Debug.Log("Need to confirm that the return is correct");
                return TriggerState.None;
            }
            ShouldCheckInput = true;
            return base.InvokeOnTriggerEnter(other);
        }
        
        protected override TriggerState InvokeOnTriggerExit() {
            if (!ShouldCheckInput) {
                Debug.Log("Need to confirm that the return is correct");
                return TriggerState.None;
            }
            ShouldCheckInput = false;
            return base.InvokeOnTriggerExit();
        }

        private bool CanCameraActivateTrigger(Vector3 cameraPosition) {
            if (Vector3.Distance(cameraPosition, transform.position) > _maxInteractDistance)  return false;
            return true;
        }
    }
}
