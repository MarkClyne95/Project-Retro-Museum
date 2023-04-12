using System.Collections;
using System.Collections.Generic;
using ScottEwing.Checkpoints;
using Unity.VisualScripting;
using UnityEngine;
//#if SE_EVENTSYSTEM
using ScottEwing.EventSystem;
using Sirenix.OdinInspector;
//#endif

namespace ScottEwing.Checkpoints
{
    /// <summary>
    /// Inherites from Movable Object. Can respawn when checkpoint is reload but can also respawn independently of checkpoints
    /// </summary>
    public class RespawnableObject : MovableObject{
        [Tooltip("The respawn position and rotation will be updated each time a checkpoint reached event is invoked. If false the default transform will be used indefinitely")]
        [SerializeField] private bool _updateRespawnTransformOnCheckpointReached = false;
        
        [Tooltip("If true the base checkpoint reload functionality will remain as well as the new checkpoint independent respawn")]
        [SerializeField] private bool _respawnOnCheckpointReload = false;


        protected override void OnCheckpointReached() {
            if (_updateRespawnTransformOnCheckpointReached) {
                base.OnCheckpointReached();
            }
        }

        protected override void OnCheckpointReset() {
            if (_updateRespawnTransformOnCheckpointReached) {
                base.OnCheckpointReset();
            }
        }
        
#if SE_EVENTSYSTEM
        protected override void OnCheckpointReached(CheckpointReachedEvent obj) {
            if (_updateRespawnTransformOnCheckpointReached) {
                base.OnCheckpointReached(obj);
            }
        }

        protected override void OnCheckpointReset(ResetToCheckpointEvent obj) {
            if (_respawnOnCheckpointReload) {
                base.OnCheckpointReset(obj);
            }
        }
#endif

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }
        }

        public void Respawn(float waitTime)
        {
            Invoke("Respawn", waitTime);
        }

        [Button]

        public override void Respawn() {
            StartCoroutine(Routine());
            //--Need to wait a frame because obi can't deactivate ropes during physics update. This was being called during on trigger enter 
            IEnumerator Routine() {
                yield return null;
  
                base.Respawn();
                var evt = new ObjectRespawnedEvent {
                    respawnedObject = gameObject,
                    childColliders = GetComponentsInChildren<Collider>()
                };
                EventManager.Broadcast(evt);
            }
        }

        public void SaveLocation(Collider checkpoint) {
            var transform = checkpoint.transform;
            SaveLocation(transform.position, transform.rotation);
        }
    }
}
