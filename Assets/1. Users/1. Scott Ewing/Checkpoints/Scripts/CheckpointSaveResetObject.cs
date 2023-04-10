using UnityEngine;
#if SE_EVENTSYSTEM
using ScottEwing.EventSystem;
#endif

namespace ScottEwing.Checkpoints {
    public abstract class CheckpointSaveResetObject : MonoBehaviour {
        protected virtual void OnEnable() {
            CheckpointManager.OnCheckpointReachedEvent += OnCheckpointReached;
            CheckpointManager.OnResetToCheckpointEvent += OnCheckpointReset;
#if SE_EVENTSYSTEM
            EventManager.AddListener<CheckpointReachedEvent>(OnCheckpointReached);
            EventManager.AddListener<ResetToCheckpointEvent>(OnCheckpointReset);

#endif
        }



        protected virtual void OnDisable() {
            CheckpointManager.OnCheckpointReachedEvent -= OnCheckpointReached;
            CheckpointManager.OnResetToCheckpointEvent -= OnCheckpointReset;
#if SE_EVENTSYSTEM
            EventManager.RemoveListener<CheckpointReachedEvent>(OnCheckpointReached);
            EventManager.RemoveListener<ResetToCheckpointEvent>(OnCheckpointReset);
#endif

        }
        protected virtual void OnCheckpointReached() {
        }
        protected virtual void OnCheckpointReset() {
        }
        
#if SE_EVENTSYSTEM
        protected virtual void OnCheckpointReached(CheckpointReachedEvent obj) {
        }
        protected virtual void OnCheckpointReset(ResetToCheckpointEvent obj) {
        }
#endif

    }
}
