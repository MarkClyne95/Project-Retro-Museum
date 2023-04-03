using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EventManager : MonoBehaviour
{
    public delegate void BadtariDestroy();
    public static event BadtariDestroy OnBadtariDestroy;

    public delegate void AtariDestroy();
    public static event AtariDestroy OnAtariDestroy;

    public delegate void NextLevel();
    public static event NextLevel OnNextLevel;

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

    public void IncrementLevel()
    {
        if(OnNextLevel != null)
        {
            OnNextLevel();
        }
    }
}
