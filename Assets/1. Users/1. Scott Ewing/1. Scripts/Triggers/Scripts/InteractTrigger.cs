using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace ScottEwing.Triggers {
    public class InteractTrigger : Trigger , ITakesInput{
        [field: SerializeField] public InputActionProperty InputActionReference { get; set; }
        public bool ShouldCheckInput { get; set; }

        private void Update() => GetInput();


        public void GetInput() {
            if (!IsActivatable) return;
            if (!ShouldCheckInput) return;
            if (InputActionReference.action == null) return;
            if (InputActionReference.action.triggered) {
                Triggered();
            }
        }
        protected override void OnTriggerEnter(Collider other) {
            if (!IsColliderValid(other)) return;
            //if (!other.CompareTag(_triggeredByTag)) return;
            ShouldCheckInput = true;
            base.OnTriggerEnter(other);
        }

        protected override void OnTriggerExit(Collider other) {
            if (!IsColliderValid(other)) return;
            //if (!other.CompareTag(_triggeredByTag)) return;
            ShouldCheckInput = false;
            base.OnTriggerExit(other);
        }
    } 
}
