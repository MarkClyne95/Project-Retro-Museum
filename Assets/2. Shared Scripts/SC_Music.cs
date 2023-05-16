using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Music : MonoBehaviour
{
    [Header("Audio Stuff")]
    private AudioSource source;
    private float volume;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("MusicVol");

        source.volume = volume * .1f;
    }
}
