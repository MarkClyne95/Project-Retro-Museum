using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelExit : MonoBehaviour{
    [SerializeField] private float exitWaitTime = 1;
    public void ExitLevel() {
        Invoke("Exit", exitWaitTime);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            Exit();
        }
    }

    public void Exit() {
        SceneManager.LoadScene("L_90sFloor");
    }
}
