using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour{
    protected S_PlayerActorController _sActorController;

    [SerializeField] private int lives = 3;
    [SerializeField] private int wrongAnswers = 0;
    [SerializeField] private float reloadTime = 1.5f;


    void Start() {
        _sActorController = FindObjectOfType<S_PlayerActorController>();
    }

    public void IncorrectAnswer() {
        wrongAnswers++;
        _sActorController.BroadcastIncorrectAnswerEvent(wrongAnswers);
        if (wrongAnswers == lives) {
            StartCoroutine(FailedRoutine());
        }

        IEnumerator FailedRoutine() {
            yield return new WaitForSeconds(reloadTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            yield return null;
        }
    }
}