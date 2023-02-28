using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class S_UiHealth : MonoBehaviour
{
    private S_ActorController _actorController;
    private TextMeshProUGUI _healthText;
    
    // Start is called before the first frame update
    void Start()
    {
        _actorController = GetComponentInParent<S_ActorController>();
        _actorController.ActorEventManager.AddListener<DamageTakenEvent>(OnTakeDamage);
        _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnActorDeath);
        _actorController.ActorEventManager.AddListener<ReceiveHealthEvent>(OnReceiveHealth);
        _healthText = GetComponent<TextMeshProUGUI>();
    }

    private void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnTakeDamage);
        _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnActorDeath);
        _actorController.ActorEventManager.RemoveListener<ReceiveHealthEvent>(OnReceiveHealth);
    }

    private void OnActorDeath(ActorDeathEvent obj) => SetText(0);
    private void OnTakeDamage(DamageTakenEvent obj) => SetText(obj.RemainingHealth);
    private void OnReceiveHealth(ReceiveHealthEvent obj) => SetText(obj.Health);

    private void SetText(int health) {
        _healthText.SetText(health + "%");
    }
    
}
