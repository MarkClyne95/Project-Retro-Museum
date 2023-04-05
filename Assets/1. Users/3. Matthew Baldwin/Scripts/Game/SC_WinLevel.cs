using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WinLevel : MonoBehaviour
{
    [Header("Score Information")]
    [SerializeField] private int scoreThreshold;
    [SerializeField] private SC_PlayerStats stats;
    [SerializeField] private SC_SpawnCart spawnCheck;

    [Header("Level Completion Thresholds")]


    [Header("Level Information")]
    [SerializeField] private SC_SwitchLevel switchLevel;

    [Header("Events")]
    [SerializeField] private SC_EventManager events;

    public static SC_WinLevel instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }else 
        {
            instance = this;
        }

        events = GameObject.FindGameObjectWithTag("Player").GetComponent<SC_EventManager>();
    }


    public void SetLevelVariables()
    {
        spawnCheck = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SC_SpawnCart>();

        switch (switchLevel.LevelIndex)
        {
            case 2:
                scoreThreshold = 6000;
                break;
            case 5:
                scoreThreshold = 11000;
                break;
            case 8:
                scoreThreshold = 17000;
                break;
        }
    }

    public void ScoreCheck()
    {
        spawnCheck.cartsAlive--;

        if (stats.PlayerScore >= scoreThreshold && spawnCheck.toSpawn == 0 && spawnCheck.cartsAlive == 0)
        {
            events.IncrementLevel();
            switchLevel.Invoke("NextScene", 3f);
        }
        else if(stats.PlayerScore < scoreThreshold && spawnCheck.toSpawn == 0 && spawnCheck.cartsAlive == 0)
        {
            events.EndGame();
            switchLevel.Invoke("FullReset", 3f);
        }
        else if(stats.endless == true)
        {
            spawnCheck.toSpawn++;
        }
    }


    private void OnEnable()
    {
        SC_EventManager.OnBadtariDestroy += ScoreCheck;
        SC_EventManager.OnAtariDestroy += ScoreCheck;
    }

    private void OnDisable()
    {
        SC_EventManager.OnBadtariDestroy -= ScoreCheck;
        SC_EventManager.OnAtariDestroy -= ScoreCheck;
    }
}
