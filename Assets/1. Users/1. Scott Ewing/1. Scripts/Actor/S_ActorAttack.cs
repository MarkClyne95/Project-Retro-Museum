using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class S_ActorAttack : MonoBehaviour
{
    
    protected S_ActorController _actorController;
    [SerializeField] protected float _minAttackCooldown = 1f;
    [SerializeField] protected float _maxAttackCooldown = 3f;

    [SerializeField] protected float _attackCooldown = 1.5f;
    [SerializeField] protected bool _attackCooldownOver = true;
    [SerializeField] protected int _damage = 10;

    [SerializeField] public float _attackDuration = 0.5f;
    [SerializeField] protected bool _requireTarget = true;
    [field: SerializeField] public bool IsAttacking { get; protected set; }
    


    protected Coroutine _attackRoutine;
    protected Coroutine _attackTimerRoutine;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _actorController = GetComponentInParent<S_ActorController>();
        _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnActorDeath);
    }
    
    protected virtual void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnActorDeath);

    }
    
    
    public virtual bool TryAttack() {
        return false;
    }
    
    
    public virtual bool CanAttack(out ITakesDamage damageTaker, out RaycastHit hit) {
        damageTaker = null;
        hit = default;
        return false;
    }
    
    protected virtual void Attack(ITakesDamage damageTaker, RaycastHit hit) {
        

    }

    protected void SetAttackCooldownTime() => _attackCooldown = Random.Range(_minAttackCooldown, _maxAttackCooldown);

    
    protected IEnumerator AttackRoutine() {
        while (true) {
            bool attacked = false;
            while (!attacked) {
                attacked = TryAttack();
                yield return null;
            }
            //yield return _actorController.StartCoroutine(AttackTimerRoutine());
            /*yield return new WaitForSeconds(_attackDuration);
            IsAttacking = false;
            yield return new WaitForSeconds(_attackCooldown);
            _attackCooldownOver = true;*/
        }
    }
    
    protected IEnumerator AttackTimerRoutine() {
        yield return new WaitForSeconds(_attackDuration);
        IsAttacking = false;
        yield return new WaitForSeconds(_attackCooldown);
        _attackCooldownOver = true;
        //_attackRoutine = null;
    }
    
    protected virtual void OnActorDeath(ActorDeathEvent obj) {
        if (_attackRoutine != null) {
            _actorController.StopCoroutine(_attackRoutine);
        }
        if (_attackTimerRoutine != null) {
            _actorController.StopCoroutine(_attackTimerRoutine);
        }
    }

    
    
    
}
