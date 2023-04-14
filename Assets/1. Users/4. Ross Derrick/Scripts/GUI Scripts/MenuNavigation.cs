using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuNavigation : MonoBehaviour {

	private void Start()
	{
		// Enable the mouse cursor and unlock it from the center of the screen
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void MainMenu()
	{
		Application.LoadLevel("menu");
	}

	public void Quit()
	{
		SceneManager.LoadScene("L_80sFloor");
		Destroy(GameObject.Find("Music Manager"));
	}
	
	public void Play()
	{
		Application.LoadLevel("game");
	}
	
	public void HighScores()
	{
		Application.LoadLevel("scores");
		
	}

    public void Credits()
    {
        Application.LoadLevel("credits");
    }

	
}
