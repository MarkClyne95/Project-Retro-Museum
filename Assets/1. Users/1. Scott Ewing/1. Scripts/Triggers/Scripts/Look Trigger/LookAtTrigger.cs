using UnityEngine;
using UnityEngine.Events;

namespace ScottEwing.Triggers {
    public class LookAtTrigger : Trigger {
        [Tooltip("The Camera looking at the trigger. Can be assigned in inspector or will be Main Camera by default")]
        [SerializeField] private Camera cameraToCheck;
        [Tooltip("Update CameraToCheck to Camera.main every frame ")]
        [SerializeField] private bool updateCameraToCheck = false;
        [Tooltip("The object the camera is looking for (will also work if the object the camera sees is chil/deep child of the target)")]
        [SerializeField] private GameObject target;
        //[SerializeField] private bool checkIfTargetIsParent = false;
        [SerializeField] float sphereCastRadius = 0.1f;
        [SerializeField] float lookDistance = 1;

        [Tooltip("Contains the layer of the object to look at as well as any layers the camera should not be able to look through")]
        [SerializeField] LayerMask sphereCastHitLayers;

        [SerializeField] private UnityEvent onLookAt;
        [SerializeField] private UnityEvent onLookAway;

        //--the check if the player is currently looking at the object. Only true while player is inside trigger
        private bool shouldCheckLookAt = false;
        private bool isLookAtOrAwayFirst = false;       // used to keep track of whether this is the first frame where the objects has been looked 
                                                        // at or looked away from

        void Start() {
            if (cameraToCheck == null) {
                cameraToCheck = Camera.main;
            }
        }

        // Update is called once per frame
        void Update() {
            //--set the camera to equal to the current camera
            if (updateCameraToCheck && Camera.main != null) {
                cameraToCheck = Camera.main;
            }

            if (!shouldCheckLookAt) {
                return;
            }
           

            //-- Only do the spherecast if the camera we want to check is the current camera
            /*if (cameraToCheck != Camera.current) {
                return;
            }*/

            //print("Do Sphere Cast");
            RaycastHit hit;
            if (Physics.SphereCast(cameraToCheck.transform.position, sphereCastRadius, cameraToCheck.transform.forward, out hit, lookDistance, sphereCastHitLayers.value)) {
                //print(hit.transform.gameObject.name);
                if (hit.transform.IsChildOf(target.transform)) {       // check if looking at the correct object
                    DoIsFirstTimeLookingAt();                   // check if this is the first frame the object is being looked at
                    if (UnityEngine.Input.GetButtonDown("Interact")) {
                        Debug.LogWarning("This should Call Triggered");
                        _onTriggered.Invoke();
                        //print("Selected");
                    }
                }
                else DoIsFirstTimeLookingAway();
            }
            else DoIsFirstTimeLookingAway();
        }

        

        //--Find out if this is the first frame in a row that the object is being looked at
        private void DoIsFirstTimeLookingAt() {
            if (!isLookAtOrAwayFirst) {
                onLookAt.Invoke();
                //print("Look At First");
            }
            isLookAtOrAwayFirst = true;
        }
        //--Find out if this is the first frame in a row that the object has been looked away from
        private void DoIsFirstTimeLookingAway() {
            if (isLookAtOrAwayFirst) {
                onLookAway.Invoke();
                //print("Look Away First");
            }
            isLookAtOrAwayFirst = false;
        }

        protected override void OnTriggerEnter(Collider other) {               // display the interact text
                                                                               //print("Enter Collider");
            if (other.CompareTag(_triggeredByTag)) {
                //print(" Player");

                shouldCheckLookAt = true;
                base.OnTriggerEnter(other);
            }
        }

        protected override void OnTriggerStay(Collider other) {
            base.OnTriggerStay(other);
        }

        protected override void OnTriggerExit(Collider other) {                // clear the interact text
            if (other.CompareTag(_triggeredByTag)) {
                shouldCheckLookAt = false;
                isLookAtOrAwayFirst = false;                                    // bool now ready to check if looket at for the first time again
                base.OnTriggerExit(other);
            }
        }
    }
}
