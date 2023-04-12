using System;
using System.Collections;
using System.Collections.Generic;
using ScottEwing.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _volume;
    [SerializeField] protected float _volumeMultiplier = 0.245f;


    void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _volume = PlayerPrefs.GetFloat("MusicVol");
        _audioSource.volume = _volume * _volumeMultiplier;
    }
    
}
