using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EventManager : MonoBehaviour
{
    public delegate void BadtariDestroy();
    public static event BadtariDestroy OnBadtariDestroy;

    public delegate void AtariDestroy();
    public static event AtariDestroy OnAtariDestroy;

    public delegate void BadtariLand();
    public static event BadtariLand OnBadtariLand;

    public delegate void AtariLand();
    public static event AtariLand OnAtariLand;

    public delegate void NextLevel();
    public static event NextLevel OnNextLevel;

    public delegate void Explosion();
    public static event Explosion OnExplosion;

    public void BadtariKilled()
    {
        if (OnBadtariDestroy != null)
            OnBadtariDestroy();
    }

    public void AtariKilled()
    {
        if (OnAtariDestroy != null)
            OnAtariDestroy();
    }

    public void BadtariLanded()
    {
        if (OnBadtariLand != null)
            OnBadtariLand();
    }

    public void AtariLanded()
    {
        if (OnAtariLand != null)
            OnAtariLand();
    }

    public void IncrementLevel()
    {
        if(OnNextLevel != null)
        {
            OnNextLevel();
        }
    }

    public void ExplosionDone()
    {
        if (OnExplosion != null)
        {
            OnExplosion();
        }
    }

}
