using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectRetroMuseum.ScottEwing{
    public class S_HealthItem : Trigger{
        //private AudioSource _audioSource;
        [SerializeField] private int _healthValue = 25;

        [Tooltip("Some health items can be picked up if the player already has enough health")]
        public bool _hasMaxAllowedHealthToPickup = true;
        //[ShowIf("_hasMaxAllowedHealthToPickup")]
        private int maxAllowedHealthToPickup = 100;

        private Trigger _trigger;

        private void Awake() {
            maxAllowedHealthToPickup = _hasMaxAllowedHealthToPickup ? 100 : 200;
        }
        // Start is called before the first frame update
        /*void Start() {
            //_audioSource = GetComponent<AudioSource>();
        }*/

        public void TryPickup(Collider other) {
            var actorHealth = other.gameObject.GetComponentInChildren<S_ActorHealth>();
            if (actorHealth == null) {
                return;
            }

            if (_hasMaxAllowedHealthToPickup && actorHealth.CurrentHealth() >= maxAllowedHealthToPickup) {
                return;
            }
            actorHealth.ReceiveHealth(_healthValue, maxAllowedHealthToPickup);
            Triggered();
            //_audioSource.Play();
        }
    }
}
