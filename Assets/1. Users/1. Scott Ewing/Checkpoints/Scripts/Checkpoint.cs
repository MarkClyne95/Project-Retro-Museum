using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using ScottEwing.Triggers;
using UnityEngine;

namespace ScottEwing.Checkpoints{
    public class Checkpoint : TouchTrigger{
        [SerializeField] private Transform _respawnTransform;
        public Vector3 RespawnPosition { get; set; }
        public Quaternion RespawnRotation { get; set; }

        private void Awake() {
            RespawnPosition = _respawnTransform.position;
            RespawnRotation = _respawnTransform.rotation;
        }

        private void OnEnable() {
            _onTriggered.AddListener(delegate { CheckpointManager.Instance.CheckpointReached(this); });
#if SE_EVENTSYSTEM
            _onTriggered.AddListener(CheckpointReached);
#endif
        }

        private void OnDisable() {
            _onTriggered.RemoveListener(delegate { CheckpointManager.Instance.CheckpointReached(this); });
#if SE_EVENTSYSTEM
            _onTriggered.RemoveListener(CheckpointReached);
#endif
        }

        private void CheckpointReached() {
            var evt = new CheckpointReachedEvent {
                position = RespawnPosition,
                rotation = RespawnRotation
            };
            EventManager.Broadcast(evt);
        }
    }
}