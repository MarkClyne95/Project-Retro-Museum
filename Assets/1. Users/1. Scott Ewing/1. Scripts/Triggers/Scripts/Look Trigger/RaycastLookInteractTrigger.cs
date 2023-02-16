using System;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace ScottEwing.Triggers{
    public enum CastSourceType{
        UseMainCameraTransform,
        AssignSourceTransform
    }
    
    public class RaycastLookInteractTrigger : LookInteractTrigger, ICastInteractor{
        [Tooltip("The game object that need to be looked at. Default is the game object this is attached to. If different, default and _lookTarget should be under same parent")]
        [SerializeField] private Transform _lookTarget = null;

        [field: SerializeField] public float SphereCastRadius { get; set; } = 0.1f;
        [field: SerializeField] public LayerMask CollisionLayers { get; set; } = ~0;
        [field: SerializeField] public QueryTriggerInteraction TriggerInteraction { get; set; } = QueryTriggerInteraction.Ignore;
        [SerializeField] private LookSource _source = new LookSource();
        
        private bool _performSphereCast;
        
        private void Start() {
            if (_lookTarget == null) {
                _lookTarget = gameObject.transform;
            }
        }
        
        protected override void FixedUpdate() {
            DoRaycast();
            base.FixedUpdate();
        }

        private void DoRaycast() {
            if (!_performSphereCast) return;
            if (!Physics.SphereCast(_source.CurrentSource.position, SphereCastRadius, _source.CurrentSource.forward, out RaycastHit hit, _maxInteractDistance, CollisionLayers.value, TriggerInteraction)) return;
            if (!hit.transform.IsChildOf(_lookTarget)) return;
            Look(_source.CurrentSource.position);
        }

        protected override void OnTriggerEnter(Collider other) {
            if (other.CompareTag(_triggeredByTag)) {
                _performSphereCast = true;
            }
        }

        protected override void OnTriggerExit(Collider other) {
            if (other.CompareTag(_triggeredByTag)) {
                _performSphereCast = false;
            }
        }
    }
}