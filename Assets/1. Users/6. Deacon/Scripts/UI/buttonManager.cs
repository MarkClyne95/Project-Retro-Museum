using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    public GameObject tiles;
    public GameObject playButton;
    public GameObject controlButton;
    public GameObject health;
    public GameObject controls;

    public void PlayGame()
    {
        tiles.SetActive(false);
        playButton.SetActive(false);
        controlButton.SetActive(false);
        health.SetActive(true);
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

    
}
