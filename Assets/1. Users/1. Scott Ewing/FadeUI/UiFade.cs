using System;
using System.Collections;
using System.ComponentModel;
using ScottEwing.ExtensionMethods;
using ScottEwing;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace ScottEwing.UI.Fade{
    public class UiFade : FakeMonoBehaviour{
        [Description("Stop Types determine what will happen when trying to start a Coroutine while the previous one is still running")]
        public enum StopTypes{
            [Description("Previous routine will continue and new routine will not start")]
            DontStopActiveRoutine,
            [Description("Previous routine will stop and new routine will start")]
            StopActiveRoutine,
            [Description("All routines running on this monobehaviour will stop and new routine will start")]
            StopAllRoutines,
            [Description("Does same as DontStopActiveRoutine. Don't pick this. Was required for as arguments default value")]
            Default
        }

        public enum StartAlphaTypes{
            
            [Description("Routine will always start from 'from Alpha' value")]
            Restart,
            [Description("Routine will always start from current alpha value")]
            StartFromCurrent,
            [Description("Does same as Restart. Don't pick this. Was required for as arguments default value")]
            Default
        }

        public const float DefaultFadeTime = 0.5f;
        public const float DefaultBetweenFadeTime = 0.5f;
        public const float DefaultBeforeFadeTime = 3f;


        private object _fadeObject;
        private Image _image;
        private CanvasGroup _canvasGroup;
        private static Coroutine _fadeRoutine;
        public readonly Ref<Coroutine> FadeRoutineRef = new Ref<Coroutine>(_fadeRoutine);

        public MonoBehaviour RunCoroutineObject { get; private set; }
        public float FadeTime { get; set; }
        public float BetweenFadeTime { get; set; }
        public float BeforeFadeTime { get; set; }

        public StopTypes StopType { get; set; }
        public StartAlphaTypes StartAlphaType { get; set; }
        public Action RoutineFinished { get; set; }
        public Action FadeInFinished { get; set; }
        public Action FadeOutFinished { get; set; }


        #region Constructors
        public UiFade(object fadeObject, float fadeTime = DefaultFadeTime, float betweenFadeTime = DefaultBetweenFadeTime, float beforeFadeTime = DefaultBeforeFadeTime, MonoBehaviour runCoroutineObject = null,
            StopTypes stopType = (StopTypes)1, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            SetObjectToFade(fadeObject);
            SetupPrivateMembers(fadeTime, betweenFadeTime, beforeFadeTime, runCoroutineObject, stopType, alphaType, routineFinished, fadeInFinished: fadeInFinished, fadeOutFinished: fadeOutFinished);
        }
        public UiFade(Image image, float fadeTime = DefaultFadeTime, float betweenFadeTime = DefaultBetweenFadeTime, float beforeFadeTime = DefaultBeforeFadeTime, MonoBehaviour runCoroutineObject = null,
            StopTypes stopType = (StopTypes)1, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            SetObjectToFade(image);
            SetupPrivateMembers(fadeTime, betweenFadeTime, beforeFadeTime, runCoroutineObject, stopType, alphaType, routineFinished, fadeInFinished, fadeOutFinished);
        }

        public UiFade(CanvasGroup canvasGroup, float fadeTime = DefaultFadeTime, float betweenFadeTime = DefaultBetweenFadeTime, float beforeFadeTime = DefaultBeforeFadeTime, MonoBehaviour runCoroutineObject = null,
            StopTypes stopType = (StopTypes)1, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            SetObjectToFade(canvasGroup);
            SetupPrivateMembers(fadeTime, betweenFadeTime, beforeFadeTime, runCoroutineObject, stopType, alphaType, routineFinished, fadeInFinished, fadeOutFinished);
        }
        
        public UiFade(Image[] imageArray, float fadeTime = DefaultFadeTime, float betweenFadeTime = DefaultBetweenFadeTime, float beforeFadeTime = DefaultBeforeFadeTime, MonoBehaviour runCoroutineObject = null,
            StopTypes stopType = (StopTypes)1, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            SetObjectToFade(imageArray);
            SetupPrivateMembers(fadeTime, betweenFadeTime, beforeFadeTime, runCoroutineObject, stopType, alphaType, routineFinished, fadeInFinished, fadeOutFinished);
        }
        
        public UiFade(CanvasGroup[] canvasGroupArray, float fadeTime = DefaultFadeTime, float betweenFadeTime = DefaultBetweenFadeTime, float beforeFadeTime = DefaultBeforeFadeTime, MonoBehaviour runCoroutineObject = null,
            StopTypes stopType = (StopTypes)1, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            SetObjectToFade(canvasGroupArray);
            SetupPrivateMembers(fadeTime, betweenFadeTime, beforeFadeTime, runCoroutineObject, stopType, alphaType, routineFinished, fadeInFinished: fadeInFinished, fadeOutFinished: fadeOutFinished);
        }
        
        private void SetupPrivateMembers(float fadeTime, float betweenFadeTime, float beforeFadeTime, MonoBehaviour runCoroutineObject, StopTypes stopType, StartAlphaTypes alphaType,
            Action routineFinished, Action fadeInFinished = null,
            Action fadeOutFinished = null) {
            FadeTime = fadeTime;
            BetweenFadeTime = betweenFadeTime;
            BeforeFadeTime = beforeFadeTime;
            RunCoroutineObject = runCoroutineObject;
            this.StopType = stopType;
            this.StartAlphaType = alphaType;
            RoutineFinished = routineFinished;
            FadeInFinished = fadeInFinished;
            FadeOutFinished = fadeOutFinished;
        }

        #endregion

        //==================================================================================================================================================================

        public Coroutine FadeIn(float? fadeTime = null, StopTypes stopType = StopTypes.Default, StartAlphaTypes alphaType = StartAlphaTypes.Default, Action routineFinished = null) {
            return Fade(0, 1, fadeTime, stopType, alphaType,routineFinished);
        }

        public Coroutine FadeOut(float? fadeTime = null, StopTypes stopType = StopTypes.Default, StartAlphaTypes alphaType = StartAlphaTypes.Default, Action routineFinished = null) {
            return Fade(1, 0, fadeTime, stopType, alphaType, routineFinished);
        }

        public Coroutine Fade(float fromAlpha, float toAlpha, float? fadeTime = null, StopTypes stopType = StopTypes.Default, StartAlphaTypes alphaType = StartAlphaTypes.Default, Action routineFinished = null) {
            NullCheckAndAssign(ref fadeTime, ref stopType, ref alphaType, ref routineFinished);
            return FadeRoutineRef.Value = Fade(fromAlpha, toAlpha, _fadeObject, (float)fadeTime, RunCoroutineObject, FadeRoutineRef, stopType, alphaType, routineFinished);
        }

        public Coroutine FadeInAndOut(float fromAlpha = 0, float toAlpha = 1, float? fadeTime = null, float? betweenFadeTime = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            fadeTime ??= FadeTime;
            betweenFadeTime ??= BetweenFadeTime;
            fadeInFinished ??= FadeInFinished;
            fadeOutFinished ??= FadeOutFinished;

            return FadeRoutineRef.Value = FadeInAndOut(_fadeObject, fromAlpha, toAlpha, (float)fadeTime, (float)betweenFadeTime, RunCoroutineObject, FadeRoutineRef, fadeInFinished, fadeOutFinished);
        }

        public Coroutine WaitThenFade(float fromAlpha = 0, float toAlpha = 1, float? fadeTime = null, float? beforeFadeTime = null, StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes alphaType = StartAlphaTypes.Default,
            Action routineFinished = null) {
            
            beforeFadeTime ??= BeforeFadeTime;
            NullCheckAndAssign(ref fadeTime, ref stopType, ref alphaType, ref routineFinished);
            return FadeRoutineRef.Value = WaitThenFade(_fadeObject, fromAlpha, toAlpha, (float)fadeTime, (float)beforeFadeTime, RunCoroutineObject, FadeRoutineRef, stopType, alphaType, routineFinished);
        }

        private void NullCheckAndAssign(ref float? fadeTime, ref StopTypes stopType, ref StartAlphaTypes alphaType, ref Action routineFinished) {
            fadeTime ??= FadeTime;
            routineFinished ??= RoutineFinished;
            if (stopType == StopTypes.Default) {
                stopType = this.StopType;
            }

            if (alphaType == StartAlphaTypes.Default) {
                alphaType = this.StartAlphaType;
            }
        }

        

        //==================================================================================================================================================================

        #region Static Methods

        #region Static User Methods

        public static Coroutine FadeImageIn(Image image, float fadeTime = DefaultFadeTime, MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null,
            StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null) {
            return Fade(0, 1, image, fadeTime, coroutineObject, thisRoutine, stopType, alphaType, routineFinished);
        }

        public static Coroutine FadeImageOut(Image image, float fadeTime = DefaultFadeTime, MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null,
            StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null) {
            return Fade(1, 0, image, fadeTime, coroutineObject, thisRoutine, stopType, alphaType, routineFinished);
        }

        public static Coroutine FadeImage(float fromAlpha, float toAlpha, Image image, float fadeTime = DefaultFadeTime, MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null,
            StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null) {
            return Fade(fromAlpha, toAlpha, image, fadeTime, coroutineObject, thisRoutine, stopType, alphaType, routineFinished);
        }

        public static Coroutine FadeCanvasGroupIn(CanvasGroup canvasGroup, float fadeTime = DefaultFadeTime, MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null,
            StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null) {
            return Fade(0, 1, canvasGroup, fadeTime, coroutineObject, thisRoutine, stopType, alphaType, routineFinished);
        }

        public static Coroutine FadeCanvasGroupOut(CanvasGroup canvasGroup, float fadeTime = DefaultFadeTime, MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null,
            StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null) {
            return Fade(1, 0, canvasGroup, fadeTime, coroutineObject, thisRoutine, stopType, alphaType, routineFinished);
        }

        public static Coroutine FadeCanvasGroup(float fromAlpha, float toAlpha, CanvasGroup canvasGroup, float fadeTime = DefaultFadeTime, MonoBehaviour coroutineObject = null,
            Ref<Coroutine> thisRoutine = null, StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes alphaType = StartAlphaTypes.Restart, Action routineFinished = null) {
            return Fade(fromAlpha, toAlpha, canvasGroup, fadeTime, coroutineObject, thisRoutine, stopType, alphaType, routineFinished);
        }

        public static Coroutine FadeImageInAndOut(Image image, float fromAlpha = 0, float toAlpha = 1, float fadeTime = DefaultFadeTime, float fadeWaitTime = DefaultBetweenFadeTime,
            MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            return FadeInAndOut(image, fromAlpha, toAlpha, fadeTime, fadeWaitTime, coroutineObject, thisRoutine, fadeInFinished, fadeOutFinished);
        }

        public static Coroutine FadeCanvasGroupInAndOut(CanvasGroup canvasGroup, float fromAlpha = 0, float toAlpha = 1, float fadeTime = DefaultFadeTime, float fadeWaitTime = DefaultBetweenFadeTime,
            MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            return FadeInAndOut(canvasGroup, fromAlpha, toAlpha, fadeTime, fadeWaitTime, coroutineObject, thisRoutine, fadeInFinished, fadeOutFinished);
        }

        #endregion

        //==================================================================================================================================================================


        /// <summary>
        /// Changes the alpha value of a given Image or Canvas group to another alpha value over a period of time. 
        /// Can pass in Actions to run once faded in and out.
        /// </summary>
        /// <param name="fromAlpha">The alpha value to start at</param>
        /// <param name="toAlpha">The alpha value to finish at</param>
        /// <param name="objectToFade">The <see cref="Image"/> or <see cref="CanvasGroup"/> which is to be faded</param>
        /// <param name="fadeTime">The time it takes to transition from one alpha to another</param>
        /// <param name="coroutineObject">The MonoBehaviour the Coroutine will run on (default will create new game object)</param>
        /// <param name="thisRoutine">Set to the Coroutine that is started. If thisRoutine is still active behavior will depend on stopType</param>
        /// <param name="stopType"> See <see cref="StopTypes"/>. Determines the behaviour when trying to start Coroutine while previous one is still running > </param>
        /// <param name="startAlphaTypes"></param>
        /// <param name="routineFinished">An Action to invoke when the Coroutine has finished fading</param>
        /// <typeparam name="T">Can only be <see cref="Image"/> or <see cref="CanvasGroup"/></typeparam>
        /// <returns>The coroutine which was started. Or thisRoutine if thisRoutine was still running and <see cref="StopTypes"/> is <see cref="StopTypes.DontStopActiveRoutine"/> ></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Coroutine Fade<T>(float fromAlpha, float toAlpha, T objectToFade, float fadeTime = DefaultFadeTime, MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null,
            StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes startAlphaTypes = StartAlphaTypes.Restart, Action routineFinished = null) where T : class {
           
            coroutineObject ??= CoroutineRunner;
            if (!HandleStopType(stopType, ref thisRoutine, coroutineObject)) return thisRoutine.Value;

            if (startAlphaTypes == StartAlphaTypes.StartFromCurrent) {
                AdjustFadeValues(GetAlpha(objectToFade), ref fromAlpha, ref toAlpha, ref fadeTime);
            }
            Coroutine temp = coroutineObject.StartCoroutine(FadeRoutine(objectToFade, fromAlpha, toAlpha, fadeTime, thisRoutine, routineFinished));
            return thisRoutine != null ? thisRoutine.Value = temp : temp;
        }

        /// <summary>
        /// Changes the alpha value of a given Image or Canvas group to another alpha value over a period of time. Waits a time then returns to original alpha value over a period of time.
        /// Can pass in Actions to run once faded in and out.
        /// </summary>
        /// <param name="objectToFade">The Image or Canvas Group object who's alpha value should be changed</param>
        /// <param name="fromAlpha">The alpha value to start at</param>
        /// <param name="toAlpha">The alpha value to finish at</param>
        /// <param name="fadeTime">The time it takes to transition from one alpha to another</param>
        /// <param name="fadeWaitTime">The time the wait at the "toAlpha" value before returning fading back to the "fromAlpha" value</param>
        /// <param name="coroutineObject">The MonoBehaviour the Coroutine will run on (default will create new game object)</param>
        /// <param name="thisRoutine">Set to the Coroutine that is started. Cannot new routine if this routine is still running</param>
        /// <param name="fadeInFinished">An Action to invoke when the Coroutine has finished fading in (fromAlpha => toAlpha)</param>
        /// <param name="fadeOutFinished">An Action to invoke when the Coroutine has finished fading out (toAlpha => fromAlpha)</param>
        /// <typeparam name="T">Can only be <see cref="Image"/> or <see cref="CanvasGroup"/> </typeparam>
        /// <returns>The coroutine which was started. Or thisRoutine if thisRoutine was still running</returns>
        public static Coroutine FadeInAndOut<T>(T objectToFade, float fromAlpha = 0, float toAlpha = 1, float fadeTime = DefaultFadeTime, float fadeWaitTime = DefaultBetweenFadeTime,
            MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null, Action fadeInFinished = null, Action fadeOutFinished = null) where T : class {
            
            if (thisRoutine != null && thisRoutine.Value != null) return thisRoutine.Value;
            coroutineObject ??= CoroutineRunner;
            Coroutine temp = coroutineObject.StartCoroutine(FadeInAndOutRoutine(objectToFade, fromAlpha, toAlpha, fadeTime, coroutineObject, fadeWaitTime, thisRoutine, fadeInFinished, fadeOutFinished));
            return thisRoutine != null ? thisRoutine.Value = temp : temp;
        }

        public static Coroutine FadeSwapAlphas<T, TV>(T objectToFadeIn, TV objectToFadeOut, float fromAlpha = 0, float toAlpha = 1, float fadeTime = DefaultFadeTime,
            Ref<Coroutine> thisRoutine = null, MonoBehaviour coroutineObject = null, Action routineFinished = null) where T : class where TV : class {
            
            if (thisRoutine != null && thisRoutine.Value != null) return thisRoutine.Value;
            coroutineObject ??= CoroutineRunner;
            var temp = coroutineObject.StartCoroutine(FadeSwapAlphasRoutine(objectToFadeIn, objectToFadeOut, fromAlpha, toAlpha, fadeTime, coroutineObject, thisRoutine, routineFinished));
            return thisRoutine != null ? thisRoutine.Value = temp : temp;
        }

        public static Coroutine WaitThenFade<T>(T objectToFade, float fromAlpha = 0, float toAlpha = 1, float fadeTime = DefaultFadeTime, float beforeFadeTime = DefaultBetweenFadeTime,
            MonoBehaviour coroutineObject = null, Ref<Coroutine> thisRoutine = null, StopTypes stopType = StopTypes.StopActiveRoutine, StartAlphaTypes startAlphaTypes  = StartAlphaTypes.Restart, Action routineFinished = null) where T : class {
            coroutineObject ??= CoroutineRunner;

            if (!HandleStopType(stopType, ref thisRoutine, coroutineObject)) 
                return thisRoutine.Value;
            
            if (startAlphaTypes == StartAlphaTypes.StartFromCurrent) {
                AdjustFadeValues(GetAlpha(objectToFade), ref fromAlpha, ref toAlpha, ref fadeTime);
            }
            Coroutine temp = coroutineObject.StartCoroutine(WaitThenFadeRoutine(objectToFade, fromAlpha, toAlpha, fadeTime, coroutineObject, beforeFadeTime, thisRoutine, routineFinished));
            return thisRoutine != null ? thisRoutine.Value = temp : temp;
        }

        private static bool HandleStopType(StopTypes stopType, ref Ref<Coroutine> thisRoutine, MonoBehaviour coroutineObject) {
            switch (stopType, thisRoutine != null && thisRoutine.Value != null) {
                case (StopTypes.DontStopActiveRoutine or StopTypes.Default, true) :
                    return false;
                case (StopTypes.StopActiveRoutine, true):
                    coroutineObject.StopCoroutine(thisRoutine.Value);
                    thisRoutine.Value = null;
                    break;
                case (StopTypes.StopAllRoutines, _):
                    coroutineObject.StopAllCoroutines();
                    thisRoutine.Value = null;
                    break;
            }
            return true;
        }

        #endregion

        //==================================================================================================================================================================

        #region Coroutines

        private static IEnumerator FadeRoutine<T>(T fadeObject, float fromAlpha, float toAlpha, float fadeTime, Ref<Coroutine> thisRoutine = null, Action routineFinished = null) {
            float time = 0;
            while (time < fadeTime) {
                SetAlpha(ref fadeObject, Mathf.Lerp(fromAlpha, toAlpha, time / fadeTime));
                time += Time.deltaTime;
                yield return null;
            }
            SetAlpha(ref fadeObject, toAlpha);
            FinishRoutine(thisRoutine, routineFinished);
        }


        private static IEnumerator FadeInAndOutRoutine<T>(T fadeObject, float fromAlpha, float toAlpha, float fadeTime, MonoBehaviour coroutineObject, float betweenFadeTime,
            Ref<Coroutine> thisRoutine = null, Action fadeInFinished = null, Action fadeOutFinished = null) {
            yield return coroutineObject.StartCoroutine(FadeRoutine(fadeObject, fromAlpha, toAlpha, fadeTime));
            fadeInFinished?.Invoke();
            yield return new WaitForSeconds(betweenFadeTime);
            yield return coroutineObject.StartCoroutine(FadeRoutine(fadeObject, toAlpha, fromAlpha, fadeTime));
            FinishRoutine(thisRoutine, fadeOutFinished);
        }

        private static IEnumerator FadeSwapAlphasRoutine<T, TV>(T fadeObjectIn, TV fadeObjectOut, float fromAlpha, float toAlpha, float fadeTime, MonoBehaviour coroutineObject,
            Ref<Coroutine> thisRoutine = null, Action routineFinished = null) {
            coroutineObject.StartCoroutine(FadeRoutine(fadeObjectIn, fromAlpha, toAlpha, fadeTime));
            yield return coroutineObject.StartCoroutine(FadeRoutine(fadeObjectOut, toAlpha, fromAlpha, fadeTime));
            FinishRoutine(thisRoutine, routineFinished);
        }

        private static IEnumerator WaitThenFadeRoutine<T>(T fadeObject, float fromAlpha, float toAlpha, float fadeTime, MonoBehaviour coroutineObject, float beforeFadeTime,
            Ref<Coroutine> thisRoutine = null, Action routineFinished = null) {
            yield return new WaitForSeconds(beforeFadeTime);
            yield return coroutineObject.StartCoroutine(FadeRoutine(fadeObject, fromAlpha, toAlpha, fadeTime));
            FinishRoutine(thisRoutine, routineFinished);
        }
        private static void FinishRoutine(Ref<Coroutine> thisRoutine, Action routineFinished) {
            if (thisRoutine != null) {
                thisRoutine.Value = null;
            }
            routineFinished?.Invoke();
        }

        #endregion

        #region Public Methods Dont Know What To Call Region
        /// <summary>
        /// Changes the object that this instance of UiFade will affect
        /// </summary>
        /// <param name="objectToFade">The new Image or Canvas Croup to fade</param>
        /// <typeparam name="T">Either an UnityEngine.UI.Image or UnityEngine.UI.CanvasGroup</typeparam>
        public void SetObjectToFade<T>(T objectToFade) where T : class{
            switch (objectToFade) {
                case Image:
                case CanvasGroup:
                case Image[]:
                case CanvasGroup[]:
                    break;
                default:
                    Debug.LogErrorFormat("Type {0} is not supported. Supported types: {1}, {2}, {3} and {4}", typeof(T), typeof(Image), typeof(CanvasGroup), typeof(Image[]), typeof(CanvasGroup));
                    return;
            }
            _fadeObject = objectToFade;
        }

        /// <summary>
        /// Sets the alpha value of an Image or Canvas Group
        /// </summary>
        public static void SetAlpha<T>(ref T objectToSet, float newAlphaValue) {
            switch (objectToSet) {
                case Image image:
                    image.SetAlpha(newAlphaValue);
                    break;
                case Image[] imageArray:
                    foreach (var img in imageArray) {
                        img.SetAlpha(newAlphaValue);
                    }
                    break;
                case CanvasGroup group:
                    group.alpha = newAlphaValue;
                    break;
                case CanvasGroup[] groupArray:
                    foreach (var group in groupArray) {
                        group.alpha = newAlphaValue;
                    }
                    break;
                default:
                    throw new ArgumentException($"This alpha value of this type cannot be modified. Only {typeof(Image)} and {typeof(CanvasGroup)} are supported", nameof(objectToSet));
            }
        }

        public void StopRoutine() {
            if (FadeRoutineRef != null && FadeRoutineRef.Value != null) {
                RunCoroutineObject ??= CoroutineRunner;
                RunCoroutineObject.StopCoroutine(FadeRoutineRef.Value);
            }
        }
        public void StopAllRoutines() {
            RunCoroutineObject ??= CoroutineRunner;
            RunCoroutineObject.StopAllCoroutines();
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Adjust the values so that when the new fade routine is started it can begin from the alpha value the previous routine was stopped at. Also adjusts fade time accordingly
        /// </summary>
        /// <remarks>Need to figure out what to do when current image alpha is outside the range of the new from/to alpha values. Should it snap to the from value or
        /// go from where it is?</remarks>
        private static void AdjustFadeValues(float currentAlpha, ref float fromAlpha, ref float toAlpha, ref float fadeTime) {
            float absDiff = Mathf.Abs(fromAlpha - toAlpha);
            float multiplier = Mathf.Abs(fromAlpha - currentAlpha) / absDiff;
            fadeTime -= (multiplier * fadeTime);
            fromAlpha = currentAlpha;
        }

        /// <summary>
        /// Gets the alpha value of an Image or Canvas Group
        /// </summary>
        /// <returns>The alpha value of an Image or CanvasGroup"</returns>>
        private static float GetAlpha<T>(T objectToFade) {
            return objectToFade switch {
                Image image => image.color.a,
                Image[] images => images[0].color.a,
                CanvasGroup canvasGroup => canvasGroup.alpha,
                CanvasGroup[] canvasGroups => canvasGroups[0].alpha,
                UiFade uiFade => GetAlpha(uiFade._fadeObject),
                _ => throw new ArgumentException("Cannot get the alpha of this type. Either it has no alpha value or code for this specific type has not been written yet", nameof(objectToFade))
            };
        }

        #endregion
    }
}