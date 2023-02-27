using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ScottEwing.MovingPlatforms{
    public class PhysicsPlatformController : MonoBehaviour{
        enum PhysicsType{
            _3D,
            _2D,
            /*TRANSFORM*/   // doesn't work (ASYNC CODE WILL STILL RUN WHEN PLAY MODE IS ENDED)
        }

        enum PlatformState{
            AtTarget,
            TowardsTarget
        }

        [SerializeField] private PhysicsType physicsType = PhysicsType._3D;

        [Tooltip("Calculate the velocity using the move time between the start position and first target position")]
        [SerializeField]
        bool calculateVelocityWithMoveTime = false;

        [ConditionalHide("calculateVelocityWithMoveTime", false, true)] [SerializeField]
        protected float maxVelocity = 3f;

        [ConditionalHide("calculateVelocityWithMoveTime", true)]
        [Tooltip("The time the platform takes to move between the start position and first target position")]
        [SerializeField]
        float moveTime = 5f;

        [Space] [SerializeField]
        protected bool useAcceleration = false; // Platforms do not currently work if this is true

        [ConditionalHide("useAcceleration", true)] [SerializeField]
        protected float acceleration = 10f; // not currently working with acceleration

        [Space] [Tooltip("An initial delay before the platform starts moving")] [SerializeField]
        protected float startDelay = 0;

        [Tooltip("The time the platform waits at each target transform before moving again")] [SerializeField]
        float waitTime = 1f;

        [Space] [Tooltip("If true platform will stop once it reaches the last Transform in the array")] [SerializeField]
        private bool singleUse = false;

        [ConditionalHide("singleUse", false, true)] [SerializeField]
        private bool loopDestinations = true; // should the platform go around the destination in a loop or should it go out and back

        [field: Tooltip("Should the platform snap to the exact target position once close enough")]
        [field: SerializeField] private bool SnapToDestination { get; set; }
        [SerializeField] private Transform[] targetTransformsArray;

        private Vector3[] _destinations;
        private Vector3 _startPosition;
        private Vector3 _currentDestination;

        private int _destinationsPosition = 0; // the current destination target in the destinations array

        private float _threshholdDistance = 0.1f; // The distance at which the platform will be considered at the target

        private float
            _lastDistanceToDestination =
                float.MaxValue; // the distance to the destination last frame used to check if the platform has 

        // ..gone past the destination.  
        private bool _isWaitTimeUp = false;
        private bool _isGoingForwards = true; // is the platform moving forwards through the destinations Array
        private bool _isPlayMode;
        
        private PlatformState _platformState;

        private BasePhysicsPlatform _physicsPlatform;

        private Coroutine _accelerateMoveRoutine;
        private Coroutine _waitTimeRoutine;

        protected virtual void Awake() {
            _startPosition = transform.position;
            _waitTimeRoutine = StartCoroutine(WaitTime(startDelay));

            SetDestinationsArray();
            if (calculateVelocityWithMoveTime) {
                CalculateMaxVelocity();
            }

            _platformState = PlatformState.AtTarget;
            _currentDestination = _destinations[0];
            _isPlayMode = true;
            InitialisePhysicsPlatform();
        }

        private void InitialisePhysicsPlatform() {
            switch (physicsType) {
                case PhysicsType._2D:
                    _physicsPlatform = new PhysicsPlatform2D(GetComponent<Rigidbody2D>());
                    break;
                case PhysicsType._3D:
                    _physicsPlatform = new PhysicsPlatform3D(GetComponent<Rigidbody>());
                    break;
                /*case PhysicsType.TRANSFORM:
                    _physicsPlatform = new TransformPlatform(GetComponent<Transform>());
                    break;*/
            }
        }

        private void SetDestinationsArray() {
            _destinations = new Vector3[targetTransformsArray.Length + 1];
            _destinations[0] = _startPosition;
            for (int i = 0; i < targetTransformsArray.Length; i++) {
                if (targetTransformsArray[i] == null) {
                    Debug.LogError(
                        "PhysicsPlatforms: A Transform in the Target Transform Array is Null. Make sure each element of the array is assign a Transform");
                    return;
                }
                _destinations[i + 1] = targetTransformsArray[i].position;
            }
        }

        // Calculate the max velocity based on how long it takes to move from start to first target position
        private void CalculateMaxVelocity() {
            float dst = Vector3.Distance(_startPosition, _destinations[1]);
            maxVelocity = dst / moveTime;
        }

        void FixedUpdate() {
            ControlPlatform();
            _physicsPlatform.ClampVelocity(maxVelocity);
        }

        private bool HasReachedDestination() {
            float dstToDest = Vector3.Distance(transform.position, _currentDestination);
            if (dstToDest < _threshholdDistance) {
                // Check if platform is close enough to destination to be considered at the destination
                _lastDistanceToDestination = float.MaxValue;
                return true;
            }

            if (dstToDest > _lastDistanceToDestination) {
                // if the platform is moving fast it may be possible for the platform to skip over the 
                _lastDistanceToDestination =
                    float.MaxValue; // .. threshold distance from one frame to the next. This will check if the distance to the
                return true; // .. destination has increased since the last frame
            }

            _lastDistanceToDestination = dstToDest;
            return false;
        }

        private void ControlPlatform() {
            switch (_platformState) {
                case PlatformState.AtTarget:
                    if (singleUse && _destinationsPosition == _destinations.Length - 1) {
                        return;
                    }

                    if (_isWaitTimeUp) {
                        SetCurrentDestination();
                        _platformState = PlatformState.TowardsTarget;

                        Vector3 moveDirection = (_currentDestination - transform.position).normalized;
                        if (useAcceleration) {
                            _accelerateMoveRoutine =
                                StartCoroutine(MoveWithAcceleration(moveDirection, maxVelocity, acceleration));
                        }
                        else {
                            MovePlatform(moveDirection, maxVelocity);
                        }
                    }

                    break;
                case PlatformState.TowardsTarget:
                    if (HasReachedDestination()) {
                        StartCoroutine(WaitTime(waitTime));
                        _platformState = PlatformState.AtTarget;
                        StartCoroutine(StopPlatform(_currentDestination));
                    }

                    break;
            }
        }

        // Sets the new current destination taking into consideration whether the platform should be going around in a loop, or going out to the last ..
        // .. destination and then back to the start the same what it came (i.e is loopDestinations true or false)
        private void SetCurrentDestination() {
            // Loop Destinations
            if (loopDestinations) {
                _destinationsPosition++;
                if (_destinationsPosition >= _destinations.Length) {
                    _destinationsPosition = 0;
                }
            }
            // Out and Back Destinations
            else {
                if (_isGoingForwards) {
                    _destinationsPosition++;
                    if (_destinationsPosition == _destinations.Length - 1) {
                        _isGoingForwards = false;
                    }
                }
                else {
                    _destinationsPosition--;
                    if (_destinationsPosition == 0) {
                        _isGoingForwards = true;
                    }
                }
            }

            _currentDestination =
                _destinations[_destinationsPosition]; // set currentDestination to correct array position
        }

        // Sets the platform velocity immediately to the max velocity
        private void MovePlatform(Vector3 direction, float force) {
            _physicsPlatform.MovePlatform(direction, force);
        }

        // Gradually increases the platform velocity to the max velocity
        private IEnumerator MoveWithAcceleration(Vector3 direction, float velocityMax, float acceleration) {
            while (_physicsPlatform.GetPlatformVelocityMagnitude() < velocityMax) {
                _physicsPlatform.MoveWithAcceleration(direction, acceleration);
                yield return new WaitForFixedUpdate();
            }
        }

        
        private IEnumerator StopPlatform(Vector3 destination) {
            // If acceleration is being used then in the eventuality in which the platform reaches the destination before reaching the max velocity, the ...
            // MoveWithAcceleration Coroutine must be stopped
            if (useAcceleration) {
                if (_accelerateMoveRoutine != null)
                    StopCoroutine(_accelerateMoveRoutine);
                yield return
                    new WaitForFixedUpdate(); // Wait a frame between telling the routine to stop and actiually setting the velocity, this ...
                // avoid a problem where the platform would continue to move slowly after reaching destination
            }

            _physicsPlatform.StopPlatform();
            if (SnapToDestination) {
                // Snap the platform to the exact destination point
                transform.position = destination;
            }
        }

        // Controls the start delay and the wait time at each destination for the platform
        private IEnumerator WaitTime(float timeToWait) {
            _isWaitTimeUp = false;
            yield return new WaitForSeconds(timeToWait);
            yield return new WaitForFixedUpdate(); // fixes a problem. Dont worry about it
            _isWaitTimeUp = true;
            _waitTimeRoutine = null;
        }

        // === LEVER PHYSICS PLATFORM CODE START ===
        private Vector3 _velocityWhenDisabled;
        public bool doStartDelayTimerOnEnable = false;

        private void OnEnable() {
            if (doStartDelayTimerOnEnable && gameObject.activeInHierarchy) {
                // && gameObject.activeInHierarchy stops null coroutine bug when play is stopped
                if (_waitTimeRoutine != null) {
                    StopCoroutine(_waitTimeRoutine);
                    _waitTimeRoutine = null;
                }
                _waitTimeRoutine = StartCoroutine(WaitTime(startDelay));
                doStartDelayTimerOnEnable = false;
            }
            if (useAcceleration) {
                _accelerateMoveRoutine =
                    StartCoroutine(MoveWithAcceleration(_velocityWhenDisabled.normalized, maxVelocity, acceleration));
            }
            else {
                _physicsPlatform.SetPlatformVelocity(_velocityWhenDisabled);
            }
        }

        private void OnDisable() {
            _velocityWhenDisabled = _physicsPlatform.GetPlatformVelocity();
            if (gameObject.activeInHierarchy) {
                StartCoroutine(StopPlatform());
            }
        }

        private IEnumerator StopPlatform() {
            // If acceleration is being used then in the eventuality in which the platform reaches the destination before reaching the max velocity, the ...
            // MoveWithAcceleration Coroutine must be stopped
            if (useAcceleration) {
                if (_accelerateMoveRoutine != null)
                    StopCoroutine(_accelerateMoveRoutine);
                yield return
                    new WaitForFixedUpdate(); // Wait a frame between telling the routine to stop and actiually setting the velocity, this ...
                // avoid a problem where the platform would continue to move slowly after reaching destination
            }
            _physicsPlatform.ClampVelocity(0);
        }

        private void OnDrawGizmos() {
            if (targetTransformsArray.Length <= 0) return;
            
            Gizmos.color = Color.blue;
            // Draw line between start position and last target transform
            Gizmos.DrawLine(_isPlayMode ? _startPosition : transform.position, targetTransformsArray[0].position);

            // Draw line between each of the target transforms
            for (int i = 0; i < targetTransformsArray.Length - 1; i++) {
                Gizmos.DrawLine(targetTransformsArray[i].position, targetTransformsArray[i + 1].position);
            }

            // Draw line between last target transform and start position
            if (!singleUse && loopDestinations) {
                // only link the last and first position if the platform will actually move along this path
                Gizmos.DrawLine(targetTransformsArray[^1].position, _isPlayMode ? _startPosition : transform.position);   // ^1 == targetTransformsArray.Length - 1
            }
        }
    }
}