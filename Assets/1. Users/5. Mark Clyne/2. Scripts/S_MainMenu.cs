using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("L_Level1");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("L_80sFloor");
    }
}
