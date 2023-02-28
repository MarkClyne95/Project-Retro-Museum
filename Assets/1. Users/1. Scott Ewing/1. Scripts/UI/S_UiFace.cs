using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;

public class S_UiFace : MonoBehaviour{
    [Serializable]
    class Data{
        public enum DamageLevel{ Lvl0 = 80,
            Lvl1 = 60, 
            Lvl2 = 40, 
            Lvl3 = 20, 
            Lvl4 = 1,
            Lvl5 = 0
        }

        //[SerializeField] private int _health;
        [SerializeField] private DamageLevel _damageLevel;
        [SerializeField] private bool _damaged;
        [SerializeField] private Sprite _sprite;

        public static DamageLevel GetDamageLevel(int health) {
            DamageLevel damageLevel = health switch {
                >= (int)DamageLevel.Lvl0 => DamageLevel.Lvl0,
                >= (int)DamageLevel.Lvl1 => DamageLevel.Lvl1,
                >= (int)DamageLevel.Lvl2 => DamageLevel.Lvl2,
                >= (int)DamageLevel.Lvl3 => DamageLevel.Lvl3,
                >= (int)DamageLevel.Lvl4 => DamageLevel.Lvl4,
                <= (int)DamageLevel.Lvl5 => DamageLevel.Lvl5
            };

            return damageLevel;
        }
        
        public bool CheckMatch(DamageLevel damageLevel, bool damaged, out Sprite sprite) {
            if (damageLevel == _damageLevel && damaged == _damaged) {
                sprite = _sprite;
                return true;
            }
            sprite = null;
            return false;
        }
    }
    private S_ActorController _actorController;
    [SerializeField] private Image _faceImage;

    [SerializeField] private List<Data> _faceSprites;
    [SerializeField] private float _damageDisplayTime = 0.75f;
    [SerializeField] private int _testHealth = 100;
    [SerializeField] private bool _testDamage = false;
    private Coroutine _returnToIdleRoutine;

    // Start is called before the first frame update

    [Button]
    public void Test() {
        _faceImage.sprite = FindSprite(_testHealth, _testDamage);
    }
    
    void Start() {
        _actorController = GetComponentInParent<S_ActorController>();
        _faceImage = GetComponent<Image>();

        _actorController.ActorEventManager.AddListener<DamageTakenEvent>(OnTakeDamage);
        _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnActorDeath);
        _actorController.ActorEventManager.AddListener<ReceiveHealthEvent>(OnReceiveHealth);


    }

    

    private void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<DamageTakenEvent>(OnTakeDamage);
        _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnActorDeath);
        _actorController.ActorEventManager.RemoveListener<ReceiveHealthEvent>(OnReceiveHealth);


    }

    

    private void OnTakeDamage(DamageTakenEvent obj) {
        _faceImage.sprite = FindSprite(obj.RemainingHealth, true);
        if (_returnToIdleRoutine != null) {
            StopCoroutine(_returnToIdleRoutine);
        }
        _returnToIdleRoutine = StartCoroutine(ReturntoIdleRoutine(obj.RemainingHealth));
    }
    
    private void OnActorDeath(ActorDeathEvent obj) => _faceImage.sprite = FindSprite(0, true);
    private void OnReceiveHealth(ReceiveHealthEvent obj) => _faceImage.sprite = FindSprite(obj.Health, false);

    public Sprite FindSprite(int health, bool damaged) {
        var damageLevel = Data.GetDamageLevel(health);
        foreach (var data in _faceSprites) {
            if (data.CheckMatch(damageLevel, damaged, out var sprite)) {
                return sprite;
            }
        }
        Debug.LogError("Face Sprite not found!", this);
        return null;
    }

    /// <summary>
    /// Waits a period of time then returns from the damaged sprite to the idle face sprite 
    /// </summary>
    IEnumerator ReturntoIdleRoutine(int health) {
        yield return new WaitForSeconds(_damageDisplayTime);
        _faceImage.sprite = FindSprite(health, false);
        _returnToIdleRoutine = null;

    }

    
}
