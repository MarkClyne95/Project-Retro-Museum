using UnityEngine;

namespace ScottEwing{
    public class FakeMonoBehaviour 
    {
        protected class CoroutineMonoBehavior : MonoBehaviour{
        }

        private static CoroutineMonoBehavior _coroutineRunner;
        protected static CoroutineMonoBehavior CoroutineRunner => _coroutineRunner != null ? _coroutineRunner : InitCoroutineRunner();

        private static CoroutineMonoBehavior InitCoroutineRunner() {
            var instance = new GameObject {
                isStatic = true
            };
            _coroutineRunner = instance.AddComponent<CoroutineMonoBehavior>();
            return _coroutineRunner;
        }
    }
}
