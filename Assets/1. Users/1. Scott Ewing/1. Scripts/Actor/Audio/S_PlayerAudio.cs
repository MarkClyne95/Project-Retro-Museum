using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;

public class S_PlayerAudio : s_ActorAudio{
    [SerializeField] private AudioClip _itemPickupClip;
    protected override void Start() {
        base.Start();
        _sActorController.ActorEventManager.AddListener<ReceiveHealthEvent>(OnReceiveHealth);
        EventManager.AddListener<ItemPickedUpEvent>(OnItemPickedUp);

    }
    protected override void OnDestroy() {
        base.OnDestroy();
        _sActorController.ActorEventManager.RemoveListener<ReceiveHealthEvent>(OnReceiveHealth);
        EventManager.RemoveListener<ItemPickedUpEvent>(OnItemPickedUp);

    }
    
    private void OnReceiveHealth(ReceiveHealthEvent obj) => _audioSource.PlayOneShot(_itemPickupClip);
    private void OnItemPickedUp(ItemPickedUpEvent obj) => _audioSource.PlayOneShot(_itemPickupClip);

}
