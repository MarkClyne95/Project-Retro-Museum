using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SwitchLevel : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private GameObject[] levels;

    [Header("ActiveLevel")]
    [SerializeField] private int levelIndex = 1;

    public int count;

    // Start is called before the first frame update
    void Start()
    {
        count = levels.Length;
    }

    public void NextScene()
    {
        levels[levelIndex].SetActive(false);
        levelIndex++;

        if (levelIndex >= count)
        {
            levelIndex = 0;
        }

        levels[levelIndex].SetActive(true);
    }

    public void FullReset()
    {
        for(int i = 0; i<=levels.Length; i++)
        {
            levels[i].SetActive(false);
        }

        levels[0].SetActive(true);
        levelIndex = 0;
    }
}
