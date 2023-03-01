using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WinLevel : MonoBehaviour
{
    [Header("Score Information")]
    [SerializeField] private int scoreThreshold;
    [SerializeField] private SC_PlayerStats stats;
    [SerializeField] private SC_SpawnCart spawnCheck;

    [Header("Level Information")]
    [SerializeField] private SC_SwitchLevel switchLevel;

    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        //coroutine = StartCoroutine(ScoreCheck());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stats.PlayerScore >= scoreThreshold && spawnCheck.toSpawn == 0 && coroutine == null)
        {
            //Go to next level
            coroutine = StartCoroutine(ScoreCheck());
        }
        else if(stats.PlayerScore < scoreThreshold && spawnCheck.toSpawn == 0)
        {
            //Game Over and reset
        }
    }

    private void CallScoreCheck()
    {
        
    }

    IEnumerator ScoreCheck()
    {
        yield return new WaitForSeconds(3f);

        switchLevel.NextScene();
    }
}
