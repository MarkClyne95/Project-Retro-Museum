using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace ScottEwing.UI.Fade{
    public class UiFadeMonoBehaviour : MonoBehaviour{
        public enum FadeObjectType{ CanvasGroup, Image, CanvasGroupArray, ImageArray }
        public UiFade UiFade { get; private set; }

        [SerializeField] public FadeObjectType _fadeObjectType = FadeObjectType.CanvasGroup;

#if ODIN_INSPECTOR
        [ShowIf("_fadeObjectType", FadeObjectType.Image)]
#endif
        [SerializeField] private Image _image;
#if ODIN_INSPECTOR
        [ShowIf("_fadeObjectType", FadeObjectType.CanvasGroup)]
#endif
        [SerializeField] private CanvasGroup _canvasGroup;
#if ODIN_INSPECTOR
        [ShowIf("_fadeObjectType", FadeObjectType.CanvasGroupArray)]
#endif
        [SerializeField] private CanvasGroup[] _canvasGroups;        
#if ODIN_INSPECTOR
        [ShowIf("_fadeObjectType", FadeObjectType.ImageArray)]
#endif
        [SerializeField] private Image[] _images;
        
        
        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private float _betweenFadeTime = 0.5f;
        [SerializeField] private float _beforeFadeTime = 3f;

        [SerializeField] private UiFade.StopTypes _stopTypes = UiFade.StopTypes.StopActiveRoutine;
        [SerializeField] private UiFade.StartAlphaTypes _startAlphaTypes = UiFade.StartAlphaTypes.Restart;

        [Tooltip("Can either fad in or fade out, not both. Fade in has priority")]
        [SerializeField] private bool _fadeInOnStart, _fadeOutOnStart;

        [SerializeField] private UnityEvent _routineFinishedEvent;
        [Tooltip("This is used as part of the fade in then out routine. It is not invoked when the individual fade in is finished")]
        [SerializeField] private UnityEvent _fadeInFinishedEvent;
        [Tooltip("This is used as part of the fade in then out routine. It is not invoked when the individual fade out is finished")]
        [SerializeField] private UnityEvent _fadeOutFinishedEvent;



        void Awake() {
            object fadeObject = null;
            switch (_fadeObjectType) {
                case FadeObjectType.CanvasGroup:
                    fadeObject = _canvasGroup;
                    break;
                case FadeObjectType.Image:
                    fadeObject = _image;
                    break;
                case FadeObjectType.CanvasGroupArray:
                    fadeObject = _canvasGroups;
                    break;
                case FadeObjectType.ImageArray:
                    fadeObject = _images;
                    break;
            }

            if (fadeObject == null) {
                Debug.LogWarning("No Fade Target Selected", this);
                return;
            }

            UiFade = new UiFade(fadeObject, _fadeTime, _betweenFadeTime, _beforeFadeTime, this, _stopTypes, _startAlphaTypes, InvokeRoutineFinishedEvent, InvokeFadeInFinishedEvent, InvokeFadeOutFinishedEvent);
        }

        private void Start() {
            if (_fadeInOnStart) {
                UiFade.FadeIn();
            }
            else if (_fadeOutOnStart) {
                UiFade.FadeOut();
            }
        }

        private void InvokeRoutineFinishedEvent() {
            _routineFinishedEvent?.Invoke();
        }
        private void InvokeFadeInFinishedEvent() {
            _fadeInFinishedEvent?.Invoke();
        }
        private void InvokeFadeOutFinishedEvent() {
            _fadeOutFinishedEvent?.Invoke();
        }
        
        public void Fade(float fromAlpha, float toAlpha) {
            UiFade.Fade(fromAlpha, toAlpha);
        }
        
        public void FadeIn() => UiFade.FadeIn();
        public void FadeOut() => UiFade.FadeOut();
        public void FadeInAndOut() => UiFade.FadeInAndOut();
        public void WaitThenFade() => UiFade.WaitThenFade();

        
    }
}
