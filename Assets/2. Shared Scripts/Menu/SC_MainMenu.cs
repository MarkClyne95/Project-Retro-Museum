using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_MainMenu : MonoBehaviour
{
    [Header("Begin Game Variables")]
    public Color loadToColour = Color.white;
    public int speed;
    [SerializeField] private string toLoad;

    [Header("Settings Menu Variables")]
    [SerializeField] private GameObject settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginGame()
    {
        Initiate.Fade(toLoad, loadToColour, speed);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
