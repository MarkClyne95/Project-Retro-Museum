using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
//using SensorToolkit.Example;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class S_PlayerActorController : S_ActorController
{
    private PlayerInput _playerInput;
    private DoomInputHandler _doomInputHandler; 

    protected override void Start() {
        base.Start();
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.actions.FindActionMap("Doom").Enable();
        _doomInputHandler = GetComponentInParent<DoomInputHandler>();
        _doomInputHandler.Fire += Fire;
    }

    protected override void OnDestroy() => _doomInputHandler.Fire -= Fire;
    private void Fire() => _actorAttack.TryAttack();

    public void BroadcastIncorrectAnswerEvent(int answers) {
        var evt = new IncorrectAnswerEvent {
            WrongAnswers = answers
        };
        ActorEventManager.Broadcast(evt);
    }
}
