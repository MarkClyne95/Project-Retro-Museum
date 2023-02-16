using System;
using UnityEngine;

namespace ScottEwing{
    public class S_Health : MonoBehaviour, ITakesDamage{
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth = 100;
        private S_ActorController _sActorController;

        private void Start() {
            _sActorController = GetComponentInParent<S_ActorController>();
        }

        public void TakeDamage(int damage) {
            _currentHealth -= damage;
            _sActorController.TakeDamageEvent(damage, _currentHealth);
            if (_currentHealth < 0) {
                _currentHealth = 0;
                _sActorController.DeathEvent(damage, _currentHealth);
            }
        }
    }
}
