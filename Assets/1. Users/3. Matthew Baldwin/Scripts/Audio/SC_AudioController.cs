using UnityEngine;

public class SC_AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource[] source;

    #region Sounds
    private void AtariDestroySFX()
    {
        source[0].PlayOneShot(source[0].clip, 2f);
    }

    private void BadtariDestroySFX()
    {
        source[1].PlayOneShot(source[1].clip, 2f);
    }

    private void SaveCartSFX()
    {
        source[2].PlayOneShot(source[2].clip, 1f);
    }

    private void LoseHealthSFX()
    {
        source[3].PlayOneShot(source[3].clip, 1f);
    }

    private void TurretShoot()
    { 
        source[4].PlayOneShot(source[4].clip, .1f);
    }

    private void LoseGame()
    {
        source[5].PlayOneShot(source[5].clip, 1f);
    }

    private void WinGame()
    {
        source[6].PlayOneShot(source[6].clip, 2f);
    }
    #endregion

    #region Listeners
    private void OnEnable()
    {
        SC_EventManager.OnBadtariDestroy += BadtariDestroySFX;
        SC_EventManager.OnAtariDestroy += AtariDestroySFX;
        SC_EventManager.OnExplosion += TurretShoot;
        SC_EventManager.OnAtariLand += SaveCartSFX;
        SC_EventManager.OnBadtariLand += LoseHealthSFX;
        SC_EventManager.OnNextLevel += WinGame;
        SC_EventManager.OnGameOver += LoseGame;
    }

    private void OnDisable()
    {
        SC_EventManager.OnBadtariDestroy -= BadtariDestroySFX;
        SC_EventManager.OnAtariDestroy -= AtariDestroySFX;
        SC_EventManager.OnExplosion -= TurretShoot;
        SC_EventManager.OnAtariLand -= SaveCartSFX;
        SC_EventManager.OnBadtariLand -= LoseHealthSFX;
        SC_EventManager.OnNextLevel -= WinGame;
        SC_EventManager.OnGameOver += LoseGame;
    }
    #endregion
}
