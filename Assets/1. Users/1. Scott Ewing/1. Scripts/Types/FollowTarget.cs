using System;
using UnityEngine;

namespace ScottEwing{
    /// <summary>
    /// This class can be attached to a game object to make it follow a target game objects Position/Rotation.
    /// </summary>
    public class FollowTarget : MonoBehaviour{
        private enum PositionOptions{ Position, XZPosition, YPosition, NoPosition }
        private enum RotationOptions{ Rotation, XZRotation, YRotation, NoRotation }

        private enum OffsetType{ World, RelativeToTargetRotation }

        [SerializeField] private Transform _positionTarget, _rotationTarget;
        [SerializeField] private UpdateOptions _updateOptions = UpdateOptions.LateUpdate;
        [SerializeField] private PositionOptions _positionOption = PositionOptions.Position;
        [SerializeField] private RotationOptions _rotationOptions = RotationOptions.Rotation;
        [SerializeField] private OffsetType _offsetType = OffsetType.RelativeToTargetRotation;
        [Tooltip("If follower is child and is offset from parent, and parent Rotation is not (0,0,0), then parent should be used as pivot, otherwise ignore parent pivot ")]
        [SerializeField] private Transform _parentPivot;

        private Vector3 _offsetPosition;

        private void Start() {
            transform.rotation.ToAngleAxis(out float angle, out Vector3 axis);
            //-- This seams to work for child/non child followers as long as the start with the same (world) rotation as the target
            /*if (_parentPivot && _parentPivot.rotation != Quaternion.identity) { 
                transform.RotateAround(_parentPivot.position, axis, -angle);
            }*/

            //-- This also seams to work for child/non child followers as long as the start with the same (world) rotation as the target
            if (_parentPivot && transform.rotation!= Quaternion.identity) {
                transform.RotateAround(_parentPivot.position, axis, -angle);
            }
            _offsetPosition = transform.position - _positionTarget.position;

        }

        private void Update() {
            if (_updateOptions == UpdateOptions.Update)
                Follow();
        }

        private void FixedUpdate() {
            if (_updateOptions == UpdateOptions.FixedUpdate)
                Follow();
        }

        private void LateUpdate() {
            if (_updateOptions == UpdateOptions.LateUpdate)
                Follow();
        }

        private void Follow() {
            var newPosition = GetNewProvisionalPosition();
            
            var thisTransform = transform;      // I think this is more efficient than repeatedly accessing transform
            switch (_positionOption) {
                case PositionOptions.Position:
                    thisTransform.position = newPosition;
                    break;
                case PositionOptions.XZPosition:
                    newPosition.y = transform.position.y;
                    thisTransform.position = newPosition;
                    break;
                case PositionOptions.YPosition:
                    newPosition.x = thisTransform.position.x;
                    newPosition.z = thisTransform.position.z;
                    thisTransform.position = newPosition;
                    break;
                case PositionOptions.NoPosition:
                    break;
            }

            switch (_rotationOptions) {
                case RotationOptions.Rotation:
                    transform.rotation = _rotationTarget.rotation;
                    break;
                case RotationOptions.XZRotation:
                    transform.rotation = Quaternion.Euler(_rotationTarget.eulerAngles.x, transform.eulerAngles.y, _rotationTarget.eulerAngles.z);
                    break;
                case RotationOptions.YRotation:
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, _rotationTarget.eulerAngles.y, transform.eulerAngles.z);
                    break;
                case RotationOptions.NoRotation:
                    break;
            }
        }
        
        private Vector3 GetNewProvisionalPosition() {
            return _offsetType switch {
                OffsetType.World => _positionTarget.position + _offsetPosition,
                OffsetType.RelativeToTargetRotation => _positionTarget.TransformPoint(_offsetPosition),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}