using System;
using ScottEwing.ExtensionMethods;
using ScottEwing.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScottEwing
{
    [Serializable]
    public class RayCastHelper{
        public enum CastType{ Raycast = 0,SphereCast = 1}
        
        [SerializeField] private CastType _castType = CastType.Raycast;
        [ShowIf("_castType", CastType.SphereCast)]
        [SerializeField] private float _sphereCastRadius = 0.1f;
        [SerializeField] private float _castDistance = 30;
        [SerializeField] private LookSource _source = new LookSource();
        [field: SerializeField] public LayerMask CollisionLayers { get; set; }
        [SerializeField] private bool _specifyTargetLayers = false;

        [field: ShowIf("_specifyTargetLayers", true)]
        [field: SerializeField] public LayerMask TargetLayers { get; set; }

        [SerializeField] private QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.UseGlobal;

        public RayCastHelper() {
        }

        public LookSource LookSource => _source;

        public RayCastHelper(LayerMask collisionLayers, LayerMask targetLayers) {
            CollisionLayers = collisionLayers;
            this.TargetLayers = targetLayers;
        }

        public bool CanSourceSeeTarget<T,TV>(T targetPosition,TV targetId, out RaycastHit hit, Transform nSource = null, CastType? nCastType = null, float? nRadius = null, float? nMaxDistance = null, 
                LayerMask? nMask = null, QueryTriggerInteraction? nTriggerInteraction = null) {
            
            var sourceTransform = nSource ? nSource : _source.CurrentSource;
            var direction = (GetPosition(targetPosition) - sourceTransform.position).normalized;
            if (!Cast( out hit,sourceTransform, nRadius, direction,nMaxDistance, nMask, nCastType, nTriggerInteraction)) {
                Debug.LogWarning("No Ray hit from Source: {" + sourceTransform + "} to Target: {"+ targetPosition + "}.");
                return false;
            }
            if (!DoesTargetMatchHit(targetId, hit)) {
                Debug.LogWarning("Ray from Source: {" + sourceTransform + "} to Target: {"+ targetPosition + "} is occluded by " + hit.transform.name);
                return false;
            }
            Debug.Log("RayCastHelper Found: " + hit.collider.name);
            return true;
        }

        public bool Cast( out RaycastHit hit, Transform nSource = null, float? nRadius = null, Vector3? nDirection = null, float? nMaxDistance = null, LayerMask? nMask = null,
            CastType? nCastType = null, QueryTriggerInteraction? nTriggerInteraction = null) {
            var source = nSource ? nSource : _source.CurrentSource;
            var direction =  nDirection ?? _source.CurrentSource.forward;
            var maxDistance = nMaxDistance ?? _castDistance;
            var mask =  nMask ?? CollisionLayers;
            var castType = nCastType ?? _castType;
            var triggerInteraction = nTriggerInteraction ?? _triggerInteraction;
            if (castType == CastType.Raycast) {
                return Physics.Raycast(source.position, direction, out hit, maxDistance, mask.value, triggerInteraction);
            }else{
                var radius = nRadius ?? _sphereCastRadius;
                return Physics.SphereCast(source.position, radius, direction, out hit, maxDistance, mask.value, triggerInteraction);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hit"></param>
        /// <param name="nSource">The new source of the ray</param>
        /// <param name="nDirection">New direction of ray. Must pass this in too if wanting to use forward direction of new source transform</param>
        /// <returns></returns>
        public bool Raycast(out RaycastHit hit, Transform nSource = null, Vector3? nDirection = null,  float? nMaxDistance = null, LayerMask? nMask = null, QueryTriggerInteraction? nTriggerInteraction = null ) {
            return Cast(out hit, nSource, nDirection: nDirection,  nMaxDistance: nMaxDistance, nMask:nMask, nCastType:CastType.Raycast, nTriggerInteraction: nTriggerInteraction);
        }
        
        public bool SphereCast(out RaycastHit hit, Transform nSource = null, float? nRadius = null, Vector3? nDirection = null,  float? nMaxDistance = null, LayerMask? nMask = null, QueryTriggerInteraction? nTriggerInteraction = null ) {
            return Cast(out hit, nSource, nRadius, nDirection, nMaxDistance, nMask, CastType.SphereCast, nTriggerInteraction);
        }
        /// <summary>
        /// Only looks for the desired layers ignores layers which would be considered obstacles. Ray will hit all collidable layers but will only return true if a layer which is
        /// specified as a target is hit
        /// </summary>
        /// <returns></returns>
        public bool RaycastForTargets(out RaycastHit hit, Transform nSource = null, Vector3? nDirection = null,  float? nMaxDistance = null, LayerMask? nMask = null, LayerMask? nTargetMask = null, QueryTriggerInteraction? nTriggerInteraction = null ) {
            if (Cast( out hit, nSource, nDirection: nDirection,  nMaxDistance: nMaxDistance, nMask:nMask, nCastType:CastType.Raycast, nTriggerInteraction: nTriggerInteraction)) {
                var targetMask =  nTargetMask ?? TargetLayers;
                if (targetMask.IsLayerInLayerMask(hit.collider.gameObject.layer)) {
                    return true;
                }
            }
            return false;
        }
        
        public static bool DoRayCastHitsMatch(RaycastHit hit1, RaycastHit hit2, bool checkDistanceTolerance = true, float distanceTolerance = 0.01f) {
            if (hit1.colliderInstanceID != hit2.colliderInstanceID) {
                return false;
            }
            if (checkDistanceTolerance && Vector3.Distance(hit1.point, hit2.point) > distanceTolerance) {
                return false;
            }
            return true;
        }
        

        #region Helper Methods

        private static Vector3 GetPosition<T>(T obj) {
            return obj switch {
                RaycastHit r => r.point,
                Transform t => t.position,
                Vector3 v => v,
                _ => throw new ArgumentException("The target position must be a " + typeof(RaycastHit) + ", " + typeof(Transform) + ", " + typeof(Vector3))
            };
        }

        private static bool DoesTargetMatchHit <T>(T targetId, RaycastHit hit) {
            return targetId switch {
                int colliderID => hit.colliderInstanceID == colliderID,
                Collider collider => hit.collider == collider,
                Transform transform => hit.collider.transform == transform,
                Rigidbody rigidbody => hit.rigidbody == rigidbody,
                GameObject gameObject => hit.transform.gameObject == gameObject,
                Vector3 position => hit.point == position,

                _ => throw new ArgumentException("The target ID must be a " + typeof(int) + ", " + typeof(Collider) + ", " + typeof(Transform) + ", " + typeof(Rigidbody) + ", " + typeof(GameObject) +
                                                 ", ")
            };
        }
        #endregion
    }
}
