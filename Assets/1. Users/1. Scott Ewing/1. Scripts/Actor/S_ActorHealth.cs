using System;
using ProjectRetroMuseum.ScottEwing;
using UnityEngine;

namespace ScottEwing{
    public class S_ActorHealth : MonoBehaviour, ITakesDamage{
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth = 100;
        protected S_ActorController _actorController;
        private GameObject bloodEffectPrefab;
        [SerializeField] private bool _canBeGibbed = true;
        [SerializeField] private int _gibDamageThreshold = 10;
        private bool disableColliderOnDeath = true;
        private Collider healthCollider;

        
        protected virtual void Start() {
            _actorController = GetComponentInParent<S_ActorController>();
            healthCollider = GetComponent<Collider>();
        }

        public bool TakeDamage(int damage, RaycastHit hit) {
            _currentHealth -= damage;
            
            if (_currentHealth > 0 ) {
                if (_actorController != null) _actorController.BroadcastTakeDamageEvent(damage, _currentHealth, hit);
            }
            else if (_currentHealth <= 0) {
                bool isGibbed = _canBeGibbed && damage >= _gibDamageThreshold;
                _currentHealth = 0;
                gameObject.layer = Layers.DeadValue();
                if (_actorController != null) _actorController.BroadcastDeathEvent(damage, _currentHealth, hit, isGibbed);
                if (disableColliderOnDeath) {
                    healthCollider.isTrigger = true;
                }
                return true;
            }
            return false;
        }
        
        public void ReceiveHealth(int health, int maxHealthFromPickup) {
            _currentHealth += health;
            _currentHealth = Mathf.Min(_currentHealth, maxHealthFromPickup);
            _actorController.BroadcastReceiveHealth(_currentHealth);
        }

        public bool IsAlive() => _currentHealth > 0;
        public int CurrentHealth() => _currentHealth;
    }
}
