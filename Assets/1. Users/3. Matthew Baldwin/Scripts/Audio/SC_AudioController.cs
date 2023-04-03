using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] audioClips;

    #region Sounds
    private void AtariDestroySFX()
    {
        source.clip = audioClips[0];
        source.PlayOneShot(source.clip, 1f);
    }

    private void BadtariDestroySFX()
    {
        source.clip = audioClips[1];
        source.PlayOneShot(source.clip, 1f);
    }

    private void SaveCartSFX()
    {
        source.clip = audioClips[2];
        source.PlayOneShot(source.clip, 1f);
    }

    private void LoseHealthSFX()
    {
        source.clip = audioClips[3];
        source.PlayOneShot(source.clip, 1f);
    }

    private void TurretShoot()
    {
        source.clip = audioClips[4];
        source.PlayOneShot(source.clip, 1f);
    }
    #endregion

    #region Listeners
    private void OnEnable()
    {
        SC_EventManager.OnBadtariDestroy += BadtariDestroySFX;
        SC_EventManager.OnAtariDestroy += AtariDestroySFX;
    }

    private void OnDisable()
    {
        SC_EventManager.OnBadtariDestroy -= BadtariDestroySFX;
        SC_EventManager.OnAtariDestroy -= AtariDestroySFX;
    }
    #endregion
}
