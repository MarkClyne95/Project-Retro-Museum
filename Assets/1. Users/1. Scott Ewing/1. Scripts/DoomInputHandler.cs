using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoomInputHandler : MonoBehaviour{
    private PlayerInput _playerInput;

    //public bool fire;

    public Action Fire;

    private void Start() {
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.actions.FindActionMap("Doom").Enable();
    }


    public void OnFire(InputValue value) {
        //Fire = value.isPressed;
        Fire?.Invoke();
    }
}