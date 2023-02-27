using UnityEngine;

namespace ScottEwing.MovingPlatforms {


    public class HideSpriteOnStart : MonoBehaviour {
        [SerializeField] private bool _hideSprite = true;
        private void Awake() {
            if (_hideSprite) {
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
