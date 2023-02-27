using UnityEngine;
namespace ScottEwing.MovingPlatforms {
    public class HideMeshOnStart : MonoBehaviour {
        [SerializeField] private bool _hideMesh = true;
        private void Awake() {
            if (_hideMesh) {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
