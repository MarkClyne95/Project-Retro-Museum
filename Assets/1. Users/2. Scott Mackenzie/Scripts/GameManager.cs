using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance; // A static reference to the GameManager instance

    public Vector3 lastCheckpointPos; // Player respawn point

    //Player starts for each level
    //[SerializeField]
    //private GameObject Temple_PlayerStart;
    //[SerializeField]
    //private GameObject TempleEscape_PlayerStart;

    public GameObject playerStart;

    [SerializeField]
    private GameObject player;

    void Awake()
    {

        //if (Instance == null) // If there is no instance already
        //{
        //    DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
        //    Instance = this;
        //}
        //else if (Instance != this) // If there is already an instance and it's not `this` instance
        //{
        //    Destroy(gameObject); // Destroy the GameObject, this component is attached to
        //}



        //----------------------------------------------------------------------------------------------------------------------------------------



        //if (SceneManager.GetActiveScene().name == "TempleEscape")
        //{
        //    Debug.Log("Setting Start Pos");
        //    lastCheckpointPos = playerStart.transform.position;
        //}

        playerStart = GameObject.FindWithTag("PlayerStart");

        //Set starting position for each level
        if (SceneManager.GetActiveScene().name == "Temple")
        {
            Debug.Log("Setting Start Pos");
            lastCheckpointPos = playerStart.transform.position;

        }

        player = GameObject.FindWithTag("Player");
        player.GetComponent<ThirdPersonLocomotion>().SetPlayerPos();

        Screen.SetResolution(800, 600, true);
    }

    //Get a reference to Scene Switcher
    private void Start()
    {


        //player.transform.position = lastCheckpointPos;
        //sceneSwitch = GameObject.FindGameObjectWithTag("SceneFader").GetComponent<SceneSwitch>();
    }


    //Reload current level when player dies
    public void OnPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //player.GetComponent<ThirdPersonLocomotion>().SetPlayerPos();
        //Debug.Log("PLAYER TRANSFORM");
        //player = GameObject.FindWithTag("Player");
        //player.GetComponent<CharacterController>().enabled = true;

        //player.transform.position = lastCheckpointPos;
    }
 

    public void EndGame()
    {
        Debug.Log("Ending Game...");
        SceneManager.LoadScene("MainMenu");
        //sceneSwitch.FadeToLevel("MainMenu");
    }
}
