using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if ODIN_INSPECTOR_3_1

#endif

#if SE_EVENTSYSTEM
using System.Diagnostics;
using ScottEwing.EventSystem;
#endif

namespace ScottEwing.Checkpoints{
    public class MovableObject : CheckpointSaveResetObject{
        [Tooltip("True: On checkpoint reload, velocity reset to value as of when checkpoint was reached. False: On checkpoint reload, velocity reset to 0. ")]
        [SerializeField] private bool _useVelocity;
        [SerializeField] private Transform _defaultRespawnTransform;

        protected enum UpdateRespawnTransformType{ UseCurrentTransform, UseCheckpointRespawnTransform}
        [SerializeField] protected UpdateRespawnTransformType _updateRespawnTransformType = UpdateRespawnTransformType.UseCurrentTransform;
        
        private Vector3 _respawnPosition;
        private Quaternion _respawnRotation;
        private Vector3 _respawnVelocity;
        private Vector3 _respawnAngularVelocity;
        private Rigidbody _rb;

        private void Awake() {
            _rb = GetComponent<Rigidbody>();
            var t =  _defaultRespawnTransform ? _defaultRespawnTransform : transform;
            _respawnPosition = t.position;
            _respawnRotation = t.rotation;
            if (_rb != null) {
                _respawnVelocity = _rb.velocity;
                _respawnAngularVelocity = _rb.angularVelocity;
            }
            else if (_useVelocity){
                Debug.LogError("Movable Object Has No Rigidbody", this);
            }
        }

        protected override void OnCheckpointReached() => SaveLocation(transform.position, transform.rotation);
        protected override void OnCheckpointReset() => Respawn();

#if SE_EVENTSYSTEM
        protected override void OnCheckpointReached(CheckpointReachedEvent obj) {
            if (_updateRespawnTransformType == UpdateRespawnTransformType.UseCurrentTransform) {
                SaveLocation(transform.position, transform.rotation);
            }
            else {
                SaveLocation(obj.position, obj.rotation);
            }
            
        }

        protected override void OnCheckpointReset(ResetToCheckpointEvent obj) => Respawn();
#endif

        protected void SaveLocation(Vector3 position, Quaternion rotation) {
            //var t = transform;
            _respawnPosition = position;
            _respawnRotation = rotation;
            if (_rb != null) {
                _respawnVelocity = _rb.velocity;
                _respawnAngularVelocity = _rb.angularVelocity;
            }
        }

        public virtual void Respawn() {

            var t = transform;
            t.position = _respawnPosition;
            t.rotation = _respawnRotation;

            if (_rb == null) return;

            if (_useVelocity) {
                _rb.velocity = _respawnVelocity;
                _rb.angularVelocity = _respawnAngularVelocity;
            }else {
                //_rb.AddForce(-_rb.velocity, ForceMode.VelocityChange);
                //_rb.AddTorque(-_rb.angularVelocity, ForceMode.VelocityChange);
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }
    }
}