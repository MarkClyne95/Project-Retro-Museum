using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using TMPro;
using UnityEngine;

public class S_UiWrongAnswers : MonoBehaviour
{
    private S_ActorController _actorController;
    [SerializeField] private GameObject _icon1, _icon2, _icon3;
    
    void Start()
    {
        _actorController = GetComponentInParent<S_ActorController>();
        _actorController.ActorEventManager.AddListener<IncorrectAnswerEvent>(OnIncorrectAnswer);
    }

    private void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<IncorrectAnswerEvent>(OnIncorrectAnswer);
    }

    private void OnIncorrectAnswer(IncorrectAnswerEvent obj) {
        switch (obj.WrongAnswers) {
            case 1:
                _icon1.SetActive(true);
                break;
            case 2:
                _icon2.SetActive(true);
                break;
            case 3:
                _icon3.SetActive(true);
                break;
        }
    }
}
