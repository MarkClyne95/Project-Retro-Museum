using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SE_GameManager : MonoBehaviour
{
    protected S_PlayerActorController _playerActorController;

    // Start is called before the first frame update
    void Start() {
        _playerActorController = FindObjectOfType<S_PlayerActorController>();
        _playerActorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
    }

    private void OnDestroy() {
        _playerActorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
    }

    private void OnDeath(ActorDeathEvent obj) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
