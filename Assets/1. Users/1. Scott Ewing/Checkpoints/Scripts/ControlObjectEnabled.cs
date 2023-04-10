using System.Collections.Generic;
using UnityEngine;

//--This script will save whether an object is active or not when a checkpoint is reached and will return the object to that 
//--state when returning to a checkpoint

namespace ScottEwing.Checkpoints {
    public class ControlObjectEnabled : CheckpointSaveResetObject {

        [Tooltip("If objects were enabled when checkpoint was reached the objects will be set (back) to enabled when checkpoint is reset and vice versa")]
        [SerializeField] protected List<GameObject> _targets = new List<GameObject>();
        protected bool[] IsEnabledOnCheckpointReached;
 
        private void Start() {
            IsEnabledOnCheckpointReached = new bool[_targets.Count];
            for (int i = 0; i < _targets.Count; i++) {
                IsEnabledOnCheckpointReached[i] = _targets[i].activeSelf;
            }
        }

        protected override void OnCheckpointReached() {
            if (IsEnabledOnCheckpointReached == null) {
                return;
            }
            for (int i = 0; i < _targets.Count; i++) {
                IsEnabledOnCheckpointReached[i] = _targets[i].activeSelf;
            }
        }

        protected override void OnCheckpointReset() {
            if (IsEnabledOnCheckpointReached == null)  return; 
            for (var i = 0; i < _targets.Count; i++) {
                _targets[i].SetActive(IsEnabledOnCheckpointReached[i]);
            }
        }
    } 
}
