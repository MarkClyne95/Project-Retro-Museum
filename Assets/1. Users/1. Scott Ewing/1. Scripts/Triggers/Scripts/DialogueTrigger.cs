using UnityEngine;

namespace ScottEwing.Triggers {
    public class DialogueTrigger : Trigger {


        [SerializeField] private string[] interactTextArray;
        private string currentInteractText;
        private bool shouldCheckForInput = false;
        private int textIndex = 0;

        private void Start() {
            if (interactTextArray.Length > 0) {
                currentInteractText = interactTextArray[0];
                textIndex = 0;
            }
        }

        private void Update() {
            if (shouldCheckForInput) {
                if (UnityEngine.Input.GetButtonDown("Interact") && IsActivatable) {
                    Triggered();
                }
            }
        }

        public void NextMessage() {
            if ((textIndex + 1 < interactTextArray.Length)) {          // < not <=  (does the nex index number exist)
                currentInteractText = interactTextArray[textIndex + 1];
                textIndex++;
            }
        }

        protected override void OnTriggerEnter(Collider other) {               // display the interact text
            if (other.CompareTag(_triggeredByTag)) {
                shouldCheckForInput = true;
                base.OnTriggerEnter(other);

            }
        }

        protected override void OnTriggerExit(Collider other) {                // clear the interact text
            if (other.CompareTag(_triggeredByTag)) {
                shouldCheckForInput = false;
                base.OnTriggerExit(other);
            }
        }
    } 
}
