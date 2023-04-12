using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoomInputHandler : MonoBehaviour{
    private PlayerInput _playerInput;
    protected S_ActorController _sActorController;

    

    //public bool fire;

    public Action Fire;

    private void Start() {
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.actions.FindActionMap("Doom").Enable();
        
        _sActorController = GetComponentInChildren<S_ActorController>();
        _sActorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
    }

    protected virtual void OnDestroy() {
        _sActorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
    }
    
    private void OnDeath(ActorDeathEvent obj) {
        var fpc = GetComponent<StarterAssets.FirstPersonController>();
        fpc.enabled = false;
        _playerInput.actions.FindActionMap("Doom").Disable();
    }


    public void OnFire(InputValue value) {
        //Fire = value.isPressed;
        Fire?.Invoke();
    }
}