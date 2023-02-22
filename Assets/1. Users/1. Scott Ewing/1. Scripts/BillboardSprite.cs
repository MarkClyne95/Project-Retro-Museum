using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour{
    private Transform tr;
    private Transform _cameraMainTransform;


    private void Start() {
        tr = transform;
        if (Camera.main != null) _cameraMainTransform = Camera.main.transform;
    }

    private void LateUpdate() {
        var cameraForward = _cameraMainTransform.forward;
        tr.forward = new Vector3(cameraForward.x, tr.forward.y, cameraForward.z);
        tr.forward = -tr.forward;
    }
}