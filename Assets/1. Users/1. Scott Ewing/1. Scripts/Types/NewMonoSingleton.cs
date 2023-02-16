using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//=== This Code comes from https://gist.github.com/onevcat/6025819

/// <summary>
/// Mono singleton Class. Extend this class to make singleton component.
/// Example: 
/// <code>
/// public class Foo : MonoSingleton<Foo>
/// </code>. To get the instance of Foo class, use <code>Foo.instance</code>
/// Override <code>Init()</code> method instead of using <code>Awake()</code>
/// from this class.
/// </summary>

namespace ScottEwing{
    public abstract class NewMonoSingleton<T> : MonoBehaviour where T : NewMonoSingleton<T>{
        private static T _instance = null;

        public static T Instance {
            get {
                // Instance requiered for the first time, we look for it
                if (_instance != null) return _instance;

                _instance = GameObject.FindObjectOfType(typeof(T)) as T;

                // Object not found, we create a temporary one
                if (_instance == null) {
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");

                    isTemporaryInstance = true;
                    _instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                    // Problem during the creation, this should not happen
                    if (_instance == null) {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }

                if (!_isInitialized) {
                    _isInitialized = true;
                    _instance.Init();
                }

                return _instance;
            }
        }

        public static bool isTemporaryInstance { private set; get; }

        private static bool _isInitialized;

        // If no other monobehaviour request the instance in an awake function
        // executing before this one, no need to search the object.
        private void Awake() {
            if (_instance == null) {
                _instance = this as T;
            }
            else if (_instance != this) {
                Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
                DestroyImmediate(this);
                return;
            }

            if (!_isInitialized) {
                DontDestroyOnLoad(gameObject);
                _isInitialized = true;
                _instance.Init();
            }
        }


        /// <summary>
        /// This function is called when the instance is used the first time
        /// Put all the initializations you need here, as you would do in Awake
        /// </summary>
        public virtual void Init() {
        }

        /// Make sure the instance isn't referenced anymore when the user quit, just in case.
        private void OnApplicationQuit() {
            _instance = null;
        }
    }
}