using System;
using ScottEwing.EventSystem;
using UnityEngine;

#if GameEvents

#endif

namespace ScottEwing.Checkpoints{
    public class CheckpointManager : NewMonoSingleton<CheckpointManager>{
        public static Action OnCheckpointReachedEvent = delegate { };
        public static Action OnResetToCheckpointEvent = delegate { };

        //--Private
        private Vector3 _respawnPosition;
        private Quaternion _respawnRotation;
        [SerializeField] private Checkpoint[] _checkpoints;

        //--Auto-Implemented Properties
        public Checkpoint CurrentCheckpoint { get; set; }

        public override void Init() {
            base.Init();
            _checkpoints = FindObjectsOfType<Checkpoint>();
        }

        //--Public Methods
        public void CheckpointReached(Checkpoint checkpointReached) {
            CurrentCheckpoint = checkpointReached;
            OnCheckpointReachedEvent?.Invoke(); // save Movable Objects
            //var evt = Events.checkpointEvents.checkpointReachedEvent;
            var evt = new CheckpointReachedEvent {
                position = CurrentCheckpoint.RespawnPosition,
                rotation = CurrentCheckpoint.RespawnRotation
            };
            EventManager.Broadcast(evt);

        }

        public void ReloadCheckpoint() {
            OnResetToCheckpointEvent?.Invoke(); // reset Movable Objects (also reset player just now)
            //CollectablesManager.Instance.SetLootUI();
            ResetToCheckpointEvent evt;
            if (CurrentCheckpoint) {
                evt = new ResetToCheckpointEvent {
                    checkpointAvailable = true,
                    position = CurrentCheckpoint.RespawnPosition,
                    rotation = CurrentCheckpoint.RespawnRotation
                };
            }
            else {
                evt = new ResetToCheckpointEvent();
            }
            EventManager.Broadcast(evt);
        }

        //--Private Methods
        private void DisableAllButCurrentCheckpoints() {
            foreach (var checkpoint in _checkpoints) {
                if (checkpoint == CurrentCheckpoint) {
                    continue;
                }
                checkpoint.DisableTriggerObject();
            }
        }
        
        private void Update() {
            if (UnityEngine.Input.GetKeyDown(KeyCode.L)) {
                ReloadCheckpoint();
            }
        }
    }
}