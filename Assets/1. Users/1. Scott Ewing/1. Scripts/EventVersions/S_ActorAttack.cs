using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using SensorToolkit;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class S_ActorAttack : MonoBehaviour{
    private S_ActorController _actorController;
    [SerializeField] private float _minAttackCooldown = 1f;
    [SerializeField] private float _maxAttackCooldown = 3f;

    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private bool _attackCooldownOver = true;
    [SerializeField] private int _damage = 10;

    [SerializeField] public float _attackDuration = 0.5f;
    [field: SerializeField] public bool IsAttacking { get; private set; }
    [SerializeField] private bool _requireTarget = true;
    private Coroutine _attackRoutine;

    [SerializeField] private RaySensor _raySensor;
    private Coroutine _attackTimerRoutine;

    public void Start() {
        _actorController = GetComponentInParent<S_ActorController>();
        _raySensor = _actorController.GetComponentInChildren<RaySensor>();
        _actorController.ActorEventManager.AddListener<StartWalkingEvent>(OnStartWalking);
        _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnActorDeath);
    }

    public void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<StartWalkingEvent>(OnStartWalking);
        _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnActorDeath);
    }

    public bool TryAttack() {
        if (!CanAttack(out var damageTaker, out var hit)) return false;
        Attack(damageTaker, hit);
        return true;
    }

    public bool CanAttack(out ITakesDamage damageTaker, out RaycastHit hit) {
        damageTaker = null;
        hit = new RaycastHit();
        if (!_attackCooldownOver) return false;
        var detectedObjects = _raySensor.GetDetected();
        if (detectedObjects.Count == 0 || !detectedObjects[0].TryGetComponent(out damageTaker)) {
            return false;
        }

        //--Only shoot if the target it hit by the ray even if there are other damage takers in the way
        if (_requireTarget && !_raySensor.IsDetected(_actorController.Target.gameObject)) {
            return false;
        }

        hit = _raySensor.GetRayHit(detectedObjects[0]);
        return true;
    }

    private void Attack(ITakesDamage damageTaker, RaycastHit hit) {
        _actorController.BroadcastAttackedEvent();

        _attackCooldownOver = false;
        IsAttacking = true;
        if (damageTaker.TakeDamage(_damage, hit)) {
            _actorController.Target = null;
            if (_attackRoutine != null) {
                _actorController.StopCoroutine(_attackRoutine);
            }
        }
        SetAttackCooldownTime();
        _attackTimerRoutine = _actorController.StartCoroutine(AttackTimerRoutine());

    }

    private void SetAttackCooldownTime() => _attackCooldown = Random.Range(_minAttackCooldown, _maxAttackCooldown);

    IEnumerator AttackRoutine() {
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

    private IEnumerator AttackTimerRoutine() {
        yield return new WaitForSeconds(_attackDuration);
        IsAttacking = false;
        yield return new WaitForSeconds(_attackCooldown);
        _attackCooldownOver = true;
        //_attackRoutine = null;
    }


    private void OnStartWalking(StartWalkingEvent obj) {
        _actorController.Target = obj.Target;
        if (_attackRoutine != null) {
            return;
        }

        _attackRoutine = _actorController.StartCoroutine(AttackRoutine());
    }

    private void OnActorDeath(ActorDeathEvent obj) {
        if (_attackRoutine != null) {
            _actorController.StopCoroutine(_attackRoutine);
        }
        if (_attackTimerRoutine != null) {
            _actorController.StopCoroutine(_attackTimerRoutine);
        }
    }
}


/*[Serializable]
public class S_ActorAttack : IActorComponent
{

private S_ActorController _actorController;
[SerializeField] private RayCastHelper _rayCastHelper;
[SerializeField] private float _minAttackCooldown = 1f;
[SerializeField] private float _maxAttackCooldown = 3f;

[SerializeField] private float _attackCooldown = 1.5f;
[SerializeField] private bool _attackCooldownOver = true;
[SerializeField] private int _damage = 10;

[SerializeField] public float _attackDuration = 0.5f;
[field: SerializeField] public bool IsAttacking { get; private set; }
[SerializeField] private bool _requireTarget = true;
private Coroutine _attackRoutine;

[SerializeField] private RaySensor _raySensor;

//private Transform _target;

public void Initialise(S_ActorController sActorController) {
    _actorController = sActorController;
    _raySensor = _actorController.GetComponentInChildren<RaySensor>();
    _actorController.ActorEventManager.AddListener<StartWalkingEvent>(OnStartWalking);
    _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnActorDeath);


    //_sActorController.StartCoroutine(TempRoutine());
}



/*IEnumerator TempRoutine() {
    while (true) {
        yield return null;
        if (Input.GetKeyDown(KeyCode.P)) {
            if (CanAttack()) {
                Attack();
            }
        }
    }
}#1#


public void OnDestroy() {
    _actorController.ActorEventManager.RemoveListener<StartWalkingEvent>(OnStartWalking);
    _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnActorDeath);

}



public bool TryAttack() {
    if (!CanAttack(out var damageTaker, out var hit)) return false;
    Attack(damageTaker, hit);
    return true;
}

public bool CanAttack(out ITakesDamage damageTaker, out RaycastHit hit) {
    damageTaker = null;
    hit = new RaycastHit();
    if (!_attackCooldownOver) return false;
    var detectedObjects = _raySensor.GetDetected(); 
    if (detectedObjects.Count == 0 || !detectedObjects[0].TryGetComponent(out damageTaker)) {
        return false;
    }

    //--Only shoot if the target it hit by the ray even if there are other damage takers in the way
    if (_requireTarget && !_raySensor.IsDetected(_actorController.Target.gameObject)) {
        return false;
    }
    hit = _raySensor.GetRayHit(detectedObjects[0]);
    return true;
}


/*private void Attack(ITakesDamage damageTaker, RaycastHit hit) {
    _sActorController.BroadcastAttackedEvent();
    
    _attackCooldownOver = false;
    IsAttacking = true;
    damageTaker.TakeDamage(_damage, hit);
    SetAttackCooldownTime();
    if (_cooldownRoutine != null) {
        _sActorController.StopCoroutine(_cooldownRoutine);
    }
    _cooldownRoutine = _sActorController.StartCoroutine(AttackTimerRoutine(_attackDuration, _attackCooldown));
}#1#

private void Attack(ITakesDamage damageTaker, RaycastHit hit) {
    _actorController.BroadcastAttackedEvent();
    
    _attackCooldownOver = false;
    IsAttacking = true;
    if (damageTaker.TakeDamage(_damage, hit)) {
        _actorController.Target = null;
        if (_attackRoutine != null) {
            _actorController.StopCoroutine(_attackRoutine);
        }
    }
    SetAttackCooldownTime();
    
}

private void SetAttackCooldownTime() => _attackCooldown = Random.Range(_minAttackCooldown, _maxAttackCooldown);

/*private IEnumerator AttackTimerRoutine(float attackDuration, float cooldownDuration) {
    yield return new WaitForSeconds(attackDuration);
    IsAttacking = false;
    yield return new WaitForSeconds(cooldownDuration);
    _attackCooldownOver = true;
    _attackRoutine = null;
}#1#

IEnumerator AttackRoutine() {
    while (true) {
        bool attacked = false;
        while (!attacked) {
            attacked = TryAttack();
            yield return null;
        }
        yield return new WaitForSeconds(_attackDuration);
        IsAttacking = false;
        yield return new WaitForSeconds(_attackCooldown);
        _attackCooldownOver = true;
    }
}


private void OnStartWalking(StartWalkingEvent obj) {
    _actorController.Target = obj.Target;
    if (_attackRoutine != null) {
        return;
    }
    _attackRoutine = _actorController.StartCoroutine(AttackRoutine());
}

private void OnActorDeath(ActorDeathEvent obj) {
    if (_attackRoutine != null) {
        _actorController.StopCoroutine(_attackRoutine);
    }
}
}*/