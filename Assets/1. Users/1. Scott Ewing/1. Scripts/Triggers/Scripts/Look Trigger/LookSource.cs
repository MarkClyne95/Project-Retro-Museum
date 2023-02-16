using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

namespace ScottEwing.Triggers{
    [Serializable]
    public class LookSource{
        [field: SerializeField] public CastSourceType CastSourceType { get; set; } = CastSourceType.UseMainCameraTransform;

        [Tooltip("Controls the position and direction of the sphere cast")]
#if ODIN_INSPECTOR
        [ShowIf("CastSourceType", CastSourceType.AssignSourceTransform)]
#endif
        [SerializeField] private Transform _sourceTransform;
        public static Transform CachedCameraMain { get; set; }
        private Transform _currentSource;
        
        public Transform CurrentSource {
            get {
                switch (CastSourceType) {
                    case CastSourceType.UseMainCameraTransform:
                        if (CachedCameraMain == null || !CachedCameraMain.gameObject.activeInHierarchy || !CachedCameraMain.gameObject.activeSelf) {
                            CachedCameraMain = Camera.main.transform;
                        }
                        return CachedCameraMain;
                    case CastSourceType.AssignSourceTransform:
                        if (_sourceTransform == null) {
                            throw new Exception("Source Transform is not assigned");
                        }
                        return _sourceTransform;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set => _currentSource = value;
        }
        
        public static void OnCameraChanged(Transform newCamera) {
            CachedCameraMain = newCamera;
        }
    }
}
