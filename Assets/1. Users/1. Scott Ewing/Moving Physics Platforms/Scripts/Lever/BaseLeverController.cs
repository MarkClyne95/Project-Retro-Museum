using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScottEwing.MovingPlatforms{

    public class BaseLeverController : MonoBehaviour{
        [FormerlySerializedAs("basePhysicsPlatform")] [SerializeField]
        private PhysicsPlatformController _physicsPlatformController;

        [SerializeField] private GameObject _handle;


        [SerializeField] private float _onRotation = -30f; // the angle of the lever on/off position
        [SerializeField] private float _rotateSpeed = 1000;
        [SerializeField] protected bool _isLeverOn = true;
        [field: SerializeField] private bool ShouldOnlyActivateOnce { get; set; } = false; // should the lever only be activatable once
        [field: SerializeField] private bool HasBeenActivated { get; set; } // has the lever already been activated

        private float _leverState; // 1 when lever is on -1 when lever is off. Can be passed into Move To Final Position Coroutine
        private Quaternion _onQuaternion;
        private Quaternion _offQuaternion;
        private bool _shouldCheckForLeverInput;
        private Coroutine _moveLeverToFinalPosition;

        private void Awake() {
            //--Add the on/ off rotation to the initial rotation of the lever to get the correct Quaternion
            Vector3 startRotation = transform.rotation.eulerAngles;
            _onQuaternion = Quaternion.Euler(startRotation + Vector3.forward * _onRotation);
            _offQuaternion = Quaternion.Euler(startRotation + Vector3.forward * -_onRotation);

            //--Set lever state to on or off
            _handle.transform.rotation = (_isLeverOn) ? _onQuaternion : _offQuaternion;
            _leverState = (_isLeverOn) ? 1f : -1f;
        }

        private void Start() {
            _physicsPlatformController.enabled = (_isLeverOn) ? true : false;
        }

        // Move the lever to a position between the off and on rotation based on player input (between -1 and 1).
        // Use this method if the player is actively holding the lever and has control over it.
        public void MoveLever(float xInput) {
            if (ShouldOnlyActivateOnce && HasBeenActivated) {
                return;
            }

            float t = xInput / 2 + 0.5f; // this line converts the xInput which will be between -1 and 1 into the appropriate number between 0 and 1 which can be used in the lerp method.
            Quaternion targetRotation = Quaternion.Lerp(_offQuaternion, _onQuaternion, t);
            _handle.transform.rotation = Quaternion.RotateTowards(_handle.transform.rotation, targetRotation, Time.deltaTime * _rotateSpeed);
        }

        // This moves the lever to its final position after the hand has released the lever
        private IEnumerator MoveLeverToFinalPosition(float xInput) {
            if (ShouldOnlyActivateOnce && HasBeenActivated) {
                yield break;
            }

            var keepLooping = true;
            var targetRotation = _onQuaternion; // on by default
            if (xInput < 0)
                targetRotation = _offQuaternion; //Set to off if input is less than 0        

            while (keepLooping) // loops until the lever is at the target position 
            {
                _handle.transform.rotation = Quaternion.RotateTowards(_handle.transform.rotation, targetRotation, Time.deltaTime * _rotateSpeed);
                if (_handle.transform.rotation == targetRotation)
                    keepLooping = false;
                yield return null;
            }

            ControlPlatform();
            HasBeenActivated = true;
            SetLeverState();
            _moveLeverToFinalPosition = null;
        }

        private void SetLeverState() {
            if (_handle.transform.rotation == _onQuaternion)
                _leverState = 1f;
            else if (_handle.transform.rotation == _offQuaternion)
                _leverState = -1f;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _physicsPlatformController.transform.position);
        }

        void ControlPlatform() {
            if (_handle.transform.rotation == _offQuaternion) {
                _physicsPlatformController.enabled = false;
                return;
            }

            if (!HasBeenActivated && !_isLeverOn) {
                _physicsPlatformController.doStartDelayTimerOnEnable = true;
            }

            _physicsPlatformController.enabled = true;
        }

        private void Update() {
            // If the lever is touching the player and the submit key is pressed then activate the lever
            if (_shouldCheckForLeverInput && UnityEngine.Input.GetButtonDown("Submit")) {
                ActivateLever();
            }
        }

        public void ActivateLever() {
            if (_moveLeverToFinalPosition != null) {
                StopCoroutine(_moveLeverToFinalPosition);
            }

            _moveLeverToFinalPosition = StartCoroutine(MoveLeverToFinalPosition(-_leverState)); // pass in - lever state to switch it to other position
        }

        #region ON_TRIGGER_ENTER/EXIT

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                _shouldCheckForLeverInput = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                _shouldCheckForLeverInput = false;
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _shouldCheckForLeverInput = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("Player")) {
                _shouldCheckForLeverInput = false;
            }
        }

        #endregion

    }
}