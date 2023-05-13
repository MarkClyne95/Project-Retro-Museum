using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_VolSettings : MonoBehaviour
{
    [Header("Volume Variables")]
    [SerializeField] private float sfxVol;
    [SerializeField] private float musicVol;

    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    [Header("Menu Variables")]
    [SerializeField] private GameObject menu;

    [Header("Audio Variables")]
    [SerializeField] private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        sfxVol = PlayerPrefs.GetFloat("SFXVol");
        musicVol = PlayerPrefs.GetFloat("MusicVol");

        if (sfxVol == 0 && musicVol == 0)
        {
            sfxVol = 1f;
            musicVol = 1f;
        }

        sfxText.text = "" + (sfxVol * 10f);
        musicText.text = "" + (musicVol * 10f);
    }

    public void SFXUp()
    {
        sfxVol += 0.1f;
        sfxVol = Mathf.Round(sfxVol * 10.0f) * .1f;

        if (sfxVol > 1)
            sfxVol = 1;

        sfxText.text = "" + (sfxVol * 10f);
    }

    public void SFXDown()
    {
        sfxVol -= 0.1f;
        sfxVol = Mathf.Round(sfxVol * 10.0f) * .1f;

        if (sfxVol < 0)
            sfxVol = 0;

        sfxText.text = "" + (sfxVol * 10f);
    }

    public void MusicUp()
    {
        musicVol += 0.1f;
        musicVol = Mathf.Round(musicVol * 10.0f) * .1f;

        if (musicVol > 1)
            musicVol = 1;

        musicText.text = "" + (musicVol * 10f);
        source.volume = musicVol * .1f;
    }

    public void MusicDown()
    {
        musicVol -= 0.1f;
        musicVol = Mathf.Round(musicVol * 10.0f) * .1f;

        if (musicVol < 0)
            musicVol = 0;

        musicText.text = "" + (musicVol * 10f);
        source.volume = musicVol * .1f;
    }

    public void MainMenu()
    {
        menu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("SFXVol", sfxVol);
        PlayerPrefs.SetFloat("MusicVol", musicVol);
        PlayerPrefs.Save();
    }

}
