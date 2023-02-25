using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodHit : MonoBehaviour{
    public void AnimationFinished() {
        Destroy(gameObject);
    }
}
