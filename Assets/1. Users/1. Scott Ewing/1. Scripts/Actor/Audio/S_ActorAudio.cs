using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class S_ActorAudio : MonoBehaviour{
    protected S_ActorController _actorController;

    [SerializeField] protected AudioSource _audioSource;

    [SerializeField] private bool _hasDamageTakenAudio = true;

    [ShowIf("_hasDamageTakenAudio")]
    [SerializeField] private AudioClip _damageTakenClip;

    [SerializeField] private bool _hasDeathAudio = true;

    [ShowIf("_hasDeathAudio")]
    [SerializeField] private AudioClip _deathClip;

    [SerializeField] private bool _hasAttackAudio = true;

    [ShowIf("_hasAttackAudio")]
    [SerializeField] private AudioClip _attackClip;

    [SerializeField] private bool _hasGibbedAudio = true;

    [ShowIf("_hasGibbedAudio")]
    [SerializeField] private AudioClip _gibbedDeathClip;

    protected float Volume;
    [SerializeField] protected float VolumeMultiplier = 0.5f;

    protected virtual void Start() {
        _actorController = GetComponentInParent<S_ActorController>();
        _actorController.ActorEventManager.AddListener<DamageTakenEvent>(OnDamageTaken);
        _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
        _actorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);
        Volume = PlayerPrefs.GetFloat("SFXVol");
    }

    protected virtual void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnDamageTaken);
        _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
        _actorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);
    }

    private void OnDamageTaken(DamageTakenEvent obj) {
        if (!_hasDamageTakenAudio) return;
        _audioSource.PlayOneShot(_damageTakenClip, Volume * VolumeMultiplier);
    }

    private void OnDeath(ActorDeathEvent obj) {
        if (obj.Gibbed) {
            if (!_hasGibbedAudio) return;
            _audioSource.PlayOneShot(_gibbedDeathClip, Volume * VolumeMultiplier);
        }
        else {
            if (!_hasDeathAudio) return;
            _audioSource.PlayOneShot(_deathClip, Volume * VolumeMultiplier);
        }
    }

    private void OnAttack(ActorAttackEvent obj) {
        if (!_hasAttackAudio) return;
        _audioSource.PlayOneShot(_attackClip, Volume * VolumeMultiplier);
    }
}