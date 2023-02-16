using System.Collections;
using System.Collections.Generic;
using ScottEwing.Triggers;
using UnityEngine;

namespace ScottEwing.Honours
{
    public class InOutTrigger : Trigger
    {
        protected override void OnTriggerEnter(Collider other) {
            if (IsColliderValid(other)) {
                base.OnTriggerEnter(other);
            }
        }
        
        protected override void OnTriggerExit(Collider other) {
            if (IsColliderValid(other)) {
                base.OnTriggerExit(other);
            }
        }
    }
}
