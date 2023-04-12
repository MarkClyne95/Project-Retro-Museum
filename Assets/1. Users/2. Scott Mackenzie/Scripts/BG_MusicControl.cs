using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_MusicControl : MonoBehaviour
{
    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();

        //backgroundMusic.volume = PlayerPrefs.GetFloat("MusicVol");
    }

}
