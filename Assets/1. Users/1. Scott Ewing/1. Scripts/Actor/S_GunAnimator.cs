using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using Unity.VisualScripting;
using UnityEngine;

public class S_GunAnimator : MonoBehaviour
{
    private S_ActorController _actorController;
    [SerializeField] private Animator _animator;
    private readonly int _attack = Animator.StringToHash("Attack");


    // Start is called before the first frame update
    void Start()
    {
        _actorController = GetComponentInParent<S_ActorController>();
        _animator = GetComponent<Animator>();

        _actorController.ActorEventManager.AddListener<ActorAttackEvent>(OnAttack);
    }

    private void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<ActorAttackEvent>(OnAttack);
    }

    private void OnAttack(ActorAttackEvent obj) {
        _animator.SetTrigger(_attack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
