using System.Collections;
using System.Collections.Generic;
using ScottEwing;
using ScottEwing.EventSystem;
using ScottEwing.UI.Fade;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PlayerHealth : S_ActorHealth
{
    
    private UiFadeMonoBehaviour _uiFade;
    [Header("Death Variables")]
    [SerializeField] private float _reloadWaitTime = 2;
    [SerializeField] private float _dropTime = 0.25f;

    [SerializeField] private float deathDropHeight = -1;
    [SerializeField] private GameObject playerObject;


    protected override void Start() {
        base.Start();
        _actorController = GetComponentInParent<S_ActorController>();
        _actorController.ActorEventManager.AddListener<ActorDeathEvent>(OnDeath);
    }

    protected virtual void OnDestroy() {
        _actorController.ActorEventManager.RemoveListener<ActorDeathEvent>(OnDeath);
    }

    private void OnDeath(ActorDeathEvent obj) {
        StartCoroutine(OnDeathRoutine());
        IEnumerator OnDeathRoutine() {
            var time = 0.0f;
            Vector3 startPosition = playerObject.transform.position;
            var targetPosition = startPosition - Vector3.down * deathDropHeight;
            while (time < _dropTime) {
                playerObject.transform.position = Vector3.Lerp(startPosition, targetPosition, time / _dropTime);
                time += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(_reloadWaitTime - _dropTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            yield return null;
        }
    }
}
