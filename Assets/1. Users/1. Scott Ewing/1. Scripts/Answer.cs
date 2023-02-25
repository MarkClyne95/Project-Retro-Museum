using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour{
    private Sprite _overrideSprite;
    private Image _image;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    public void ChangeSprite() {
        _image.overrideSprite = _overrideSprite;
    }
}
