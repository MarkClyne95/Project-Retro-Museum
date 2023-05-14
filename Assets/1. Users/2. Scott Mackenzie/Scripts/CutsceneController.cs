using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneController : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    [SerializeField] private AudioSource randySquawkHigh;
    [SerializeField] private AudioSource randySquawkLow;

    // Start is called before the first frame update
    void Start()
    {
        randySquawkHigh.volume = PlayerPrefs.GetFloat("SFXVol");
        randySquawkLow.volume = PlayerPrefs.GetFloat("SFXVol");

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void RandyFall()
    {
        randySquawkHigh.Play();
        ActivateCameraShake();
    }

    public void RandyExclaim()
    {
        randySquawkLow.Play();
    }

    private void ActivateCameraShake()
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
    }
}
