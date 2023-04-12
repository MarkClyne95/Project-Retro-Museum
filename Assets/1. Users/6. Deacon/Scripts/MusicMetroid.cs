using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMetroid : MonoBehaviour
{
    public float volume;
    void Start()
    {
        volume = PlayerPrefs.GetFloat("MusicVol");
        this.gameObject.GetComponent<AudioSource>().volume = volume * 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
