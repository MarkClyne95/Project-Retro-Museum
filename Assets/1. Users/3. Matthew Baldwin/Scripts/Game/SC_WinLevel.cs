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
    }


    public void SetLevelVariables()
    {
        spawnCheck = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SC_SpawnCart>();
    }

    public void ScoreCheck()
    {
        spawnCheck.cartsAlive--;

        if (stats.PlayerScore >= scoreThreshold && spawnCheck.toSpawn == 0 && spawnCheck.cartsAlive == 0)
        {
            switchLevel.Invoke("NextScene", 3f);
        }
    }

}
