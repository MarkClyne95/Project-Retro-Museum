using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelExit : MonoBehaviour{
    [SerializeField] private float exitWaitTime = 1;
    public void ExitLevel() {
        Invoke("Exit", exitWaitTime);
    }

    public void Exit() {
        SceneManager.LoadScene(1);
    }
}
