using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using ScottEwing;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Gun : MonoBehaviour{
    [SerializeField] private RayCastHelper _rayCastHelper;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _reloadTime = 0.1f;
    [SerializeField] private bool _isReloading;
    [SerializeField] private int _damage = 10;
    private PlayerInput _playerInput;
    private readonly int _isFiring = Animator.StringToHash("isFiring");
    private readonly int _isFiring0 = Animator.StringToHash("isFiring0");

    private DoomInputHandler _doomInputHandler; 
    
    
    private void Start() {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.actions.FindActionMap("Doom").Enable();
        _doomInputHandler = GetComponentInParent<DoomInputHandler>();
        _doomInputHandler.Fire += Fire;
    }

    private void OnDestroy() {
        _doomInputHandler.Fire -= Fire;
    }


    private void Fire() {
        /*if (_animator.GetBool(_isFiring)) {
            return;
        }*/

        if (_isReloading) {
            return;
        }
        _animator.SetTrigger(_isFiring0);
        _isReloading = true;
        //_animator.SetBool(_isFiring, true);
        
        
        
        if (_rayCastHelper.RaycastForTargets(out var hit) && hit.collider.gameObject.TryGetComponent(out ITakesDamage damageTaker)) {
            damageTaker.TakeDamage(_damage, hit);
        }

        
        StartCoroutine(ReloadTimerRoutine());
    }

    private IEnumerator ReloadTimerRoutine() {
        yield return new WaitForSeconds(_reloadTime);
        _animator.SetBool(_isFiring, false);
        _isReloading = false;

    }
    
    
    
}
