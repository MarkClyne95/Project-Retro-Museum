using ScottEwing.Triggers;
using UnityEngine;

namespace ScottEwing.Triggers{
    public class TriggerDemo : MonoBehaviour{
        public void Look(Vector3 cameraPosition) {
        }

        public void TriggerEnter() {
            print(gameObject.name + ": Look Enter");
        }

        public void TriggerStay() {
            print(gameObject.name + ": Look Stay");
        }

        public void TriggerExit() {
            print(gameObject.name + ": Look Exit");
        }

        public void Triggered() {
            print(gameObject.name + ": Look Triggered");
        }
    }
}
