using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    public GameObject tiles;
    public GameObject playButton;
    public GameObject controlButton;
    public GameObject health;
    public GameObject controls;
    public GameObject exitButton;


    public void PlayGame()
    {
        tiles.SetActive(false);
        playButton.SetActive(false);
        controlButton.SetActive(false);
        health.SetActive(true);
        exitButton.SetActive(false);
    }

    public void ControlButton()
    {
        playButton.SetActive(false);
        controlButton.SetActive(false);
        controls.SetActive(true);
    }

    public void BackButton()
    {
        playButton.SetActive(true);
        controlButton.SetActive(true);
        controls.SetActive(false);
    }

    public void ExitButton()
    {
        //Go Back To Hub
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        MetroidScoring.totalScore = 0;
    }

    
}
