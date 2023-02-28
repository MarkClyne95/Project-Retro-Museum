using System;
using ProjectRetroMuseum.ScottEwing;
using UnityEngine;

namespace ScottEwing{
    /*[Serializable]
    public class S_ActorHealth : IActorComponent ,ITakesDamage{
        private S_ActorController _sActorController;
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth = 100;
        [SerializeField] private Collider _damageCollider;
        
        public void Initialise(S_ActorController sActorController) {
            _sActorController = sActorController;
        }

        public void OnDestroy() {
        }

        public void TakeDamage(int damage) {
            _currentHealth -= damage;
            _sActorController.BroadcastTakeDamageEvent(damage, _currentHealth);
            if (_currentHealth <= 0) {
                _currentHealth = 0;
                _damageCollider.gameObject.layer = Layers.DeadValue();
                _sActorController.BroadcastDeathEvent(damage, _currentHealth);
            }
        }
    }*/
    
    public class S_ActorHealth : MonoBehaviour, ITakesDamage{
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth = 100;
        private S_ActorController _sActorController;
        private GameObject bloodEffectPrefab;
        [SerializeField] private bool _canBeGibbed = true;
        [SerializeField] private int _gibDamageThreshold = 10;
        private bool disableColliderOnDeath = true;
        private Collider healthCollider;

        
        private void Start() {
            _sActorController = GetComponentInParent<S_ActorController>();
            healthCollider = GetComponent<Collider>();
        }

        public bool TakeDamage(int damage, RaycastHit hit) {
            _currentHealth -= damage;
            
            if (_currentHealth > 0 ) {
                if (_sActorController != null) _sActorController.BroadcastTakeDamageEvent(damage, _currentHealth, hit);
            }
            else if (_currentHealth <= 0) {
                bool isGibbed = _canBeGibbed && damage >= _gibDamageThreshold;
                _currentHealth = 0;
                gameObject.layer = Layers.DeadValue();
                if (_sActorController != null) _sActorController.BroadcastDeathEvent(damage, _currentHealth, hit, isGibbed);
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
            _sActorController.BroadcastReceiveHealth(_currentHealth);
        }

        public bool IsAlive() => _currentHealth > 0;
        public int CurrentHealth() => _currentHealth;

    }
    
}
