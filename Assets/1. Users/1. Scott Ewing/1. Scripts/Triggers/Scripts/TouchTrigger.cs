using UnityEngine;

namespace ScottEwing.Triggers {
    public class TouchTrigger : Trigger {
        protected override void OnTriggerEnter(Collider other) {
            if (!IsColliderValid(other)) return;
            Triggered();
            InvokeOnTriggerEnter();
        }
    } 
}
