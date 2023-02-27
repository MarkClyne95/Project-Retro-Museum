using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using Unity.VisualScripting;
using UnityEngine;

public class S_Nukage : MonoBehaviour{
    private ITakesDamage _target;
    private Coroutine _damageRoutine;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _cooldownTime = 1;
    [SerializeField] private bool _cooldownOver = false;


    /*public void StartDamage() {
        print("StartDamage");
        
    }*/

    IEnumerator DamageRoutine(ITakesDamage takesDamage) {
        while (true) {
            yield return new WaitForSeconds(_cooldownTime);
            if (_target.IsAlive()) {
                _target.TakeDamage(_damage, new RaycastHit());
            }
            else {
                _damageRoutine = null;
                yield break;
            }
        }
    }

    /*public void EndDamage() {
        print("EndDamage");
    }*/

    private void OnTriggerEnter(Collider other) {
        print("Trigger IN");
        _target = other.gameObject.GetComponentInChildren<ITakesDamage>();
        if (_target != null) {
            _damageRoutine = StartCoroutine(DamageRoutine(_target));
        }
    }


    private void OnTriggerExit(Collider other) {
        print("Trigger OUT");
        if (_damageRoutine != null) {
            StopCoroutine(_damageRoutine);
            _damageRoutine = null;
        }
        _target = null;
    }
}