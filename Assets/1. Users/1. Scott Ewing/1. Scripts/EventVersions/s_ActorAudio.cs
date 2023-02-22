using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class s_ActorAudio : IActorComponent
{
    private S_ActorController _sActorController;

    [SerializeField] private AudioSource _audioSource;
    
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


    public s_ActorAudio(S_ActorController sActorController) {
        //_sActorController = sActorController;
    }

    public void Initialise(S_ActorController sActorController) {
        _sActorController = sActorController;
        _sActorController.ActorEventManager.AddListener<DamageTakenEvent>(OnDamageTaken);
        _sActorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
        _sActorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);


    }
    
    public void OnDestroy() {
        _sActorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnDamageTaken);
        _sActorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
        _sActorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);

    }

    private void OnDamageTaken(DamageTakenEvent obj) {
        if (!_hasDamageTakenAudio) return;
        //_audioSource.clip = _damageTakenClip;
        _audioSource.PlayOneShot(_damageTakenClip);
    }
    private void OnDeath(ActorDeathEvent obj) {
        if (obj.Gibbed) {
            if (!_hasGibbedAudio) return;
            _audioSource.PlayOneShot(_gibbedDeathClip);
        }
        else {
            if (!_hasDeathAudio) return;
            _audioSource.PlayOneShot(_deathClip);
        }
    }
    
    private void OnAttack(ActorAttackEvent obj) {
        if (!_hasAttackAudio) return;
        //_audioSource.clip = _attackClip;
        //_audioSource.Play();
        _audioSource.PlayOneShot(_attackClip);

    }

}
