using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private FirstPersonLook camera;
    [SerializeField] private GameObject gamesMenu;

    public void ExitGame()
    {
        SceneManager.LoadScene("L_MainMenu");
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            camera.sensitivity = 2;
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            camera.sensitivity = 0;
        }
    }

    public void OpenGamesScreen()
    {
        gamesMenu.SetActive(true);
    }

    public void CloseGamesScreen()
    {
        gamesMenu.SetActive(false);
    }
}
