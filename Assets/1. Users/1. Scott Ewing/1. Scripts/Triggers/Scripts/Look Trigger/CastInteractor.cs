using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ScottEwing.Triggers{
    public class CastInteractor : MonoBehaviour, ICastInteractor{
        [field: SerializeField] public RayCastHelper Caster { get; set; } = new RayCastHelper();

        [field: SerializeField] public float SphereCastRadius { get; set; } = 0.1f;
        [field: SerializeField] public LayerMask CollisionLayers { get; set; } = ~0;
        [field: SerializeField] public QueryTriggerInteraction TriggerInteraction { get; set; } = QueryTriggerInteraction.Ignore;

        [SerializeField] private bool isDebug = false;
        
        // It would be nice to use update options but would need to make sure that the same option is picked in LookInteractTrigger
        // When checking calling HandleFirstUpdateNotLookedAt
        // Decided to use but still Untested
        [SerializeField] private UpdateOptions _updateOptions = UpdateOptions.FixedUpdate;
        private void Update() {
            if (_updateOptions == UpdateOptions.Update) { LookForInteractable(); }
        }
        private void LateUpdate() {
            if (_updateOptions == UpdateOptions.LateUpdate) { LookForInteractable(); }
        }
        private void FixedUpdate() {
            if (_updateOptions == UpdateOptions.FixedUpdate) { LookForInteractable(); }
        }
       
        //private void FixedUpdate() => LookForInteractable();

        public TriggerState? LookForInteractable(out ILookInteractable interactable) {
            if (isDebug) {
                Debug.Log("Trying Cast", this);
            }
            interactable = null;
            if (!Caster.Cast(out var castHit)) {
                return null;
            }

            if (isDebug) {
                Debug.Log("Cast Successful, TryGetComponent", this);
                
            }
            bool gotComp = castHit.collider.TryGetComponent(out interactable);
            if (!gotComp) {
                if (isDebug) {
                    Debug.Log("Couldn't find Look Interactable. Hit: " + castHit.collider.name, this);
                }
                return null;
            }
            if (isDebug) {
                Debug.Log("Looking at" + interactable, this);
            }
            //return interactable.Look(Vector3.zero);       // I cannot fathom why i was using (0,0,0) as the camera position
            return interactable.Look(Caster.LookSource.CurrentSource.position);

        }

        public TriggerState? LookForInteractable() => LookForInteractable(out ILookInteractable interactable);

        // should work trigger this but untested
        /*public bool TryTriggerInteractable() {
            TriggerState? triggerState = LookForInteractable(out ILookInteractable interactable);
            switch (triggerState) {
                case TriggerState.Enter:
                case TriggerState.Stay:
                    return (LookInteractTrigger)interactable.TryTrigger();
                    
                case TriggerState.Exit:
                case TriggerState.None:
                case null:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }*/
        
    }
}