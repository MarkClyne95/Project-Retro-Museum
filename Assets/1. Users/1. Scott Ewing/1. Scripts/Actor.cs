using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using UnityEngine;

namespace ScottEwing{
    public class Actor : MonoBehaviour, ITakesDamage{
        //Health
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth = 100;
        
        //--Animations
        [SerializeField] private Animator _animator;
        private readonly int _death = Animator.StringToHash("Death");
        private readonly int _fire = Animator.StringToHash("Fire");
        private readonly int _isWalking = Animator.StringToHash("IsWalking");

        private void Start() {
            _animator = GetComponent<Animator>();
        }

        public bool TakeDamage(int damage, RaycastHit raycastHit) {
            _currentHealth -= damage;
            
            //_sActorController.TakeDamageEvent(damage, _currentHealth);
            if (_currentHealth < 0) {
                _currentHealth = 0;
                //_sActorController.DeathEvent(damage, _currentHealth);
            }

            return false;
        }
    }
}
