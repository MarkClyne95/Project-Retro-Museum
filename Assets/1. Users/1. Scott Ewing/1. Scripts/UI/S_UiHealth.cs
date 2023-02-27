using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using TMPro;
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

        _healthText = GetComponent<TextMeshProUGUI>();
    }

    private void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnTakeDamage);
        _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnActorDeath);

    }

    private void OnActorDeath(ActorDeathEvent obj) {
        _healthText.SetText("0%");

    }

    private void OnTakeDamage(DamageTakenEvent obj) {
        _healthText.SetText(obj.RemainingHealth.ToString() + "%");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
