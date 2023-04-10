using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

namespace ScottEwing.Checkpoints{
    public class PlayerRespawn : MonoBehaviour{
        //=== Private Variables ===
        private Vector3 _respawnVelocity;
        private Vector3 _respawnAngularVelocity;
        private Rigidbody _rb;

        //=== Unity Methods ===
        private void Awake() {
            _rb = GetComponent<Rigidbody>();
            if (_rb != null) {
                _respawnVelocity = _rb.velocity;
                _respawnAngularVelocity = _rb.angularVelocity;
            }
            else {
                Debug.Log("Movable Object Has No Rigidbody");
            }
        }


        private void OnEnable() {
            CheckpointManager.OnResetToCheckpointEvent += Respawn;
#if SE_EVENTSYSTEM
            EventManager.AddListener<ResetToCheckpointEvent>(Respawn);
#endif
        }


        private void OnDisable() {
            CheckpointManager.OnResetToCheckpointEvent -= Respawn;
#if SE_EVENTSYSTEM
            EventManager.RemoveListener<ResetToCheckpointEvent>(Respawn);
#endif
        }

        

        private void Respawn() {
            if (CheckpointManager.Instance.CurrentCheckpoint != null) {
                transform.position = CheckpointManager.Instance.CurrentCheckpoint.RespawnPosition;
                transform.rotation = CheckpointManager.Instance.CurrentCheckpoint.RespawnRotation;
                if (_rb != null) {
                    _rb.AddForce(-_rb.velocity, ForceMode.VelocityChange);
                    _rb.AddTorque(-_rb.angularVelocity, ForceMode.VelocityChange);
                }
            }
            else {
                Debug.LogError("No Checkpoint for Player to Respawn at");
            }

        }
        
        private void Respawn(ResetToCheckpointEvent obj) {
            
        }
    }
}
