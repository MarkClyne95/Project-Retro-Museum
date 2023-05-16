using System;
using _2._Shared_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_GameScreen : MonoBehaviour
{
    public int index;
    [SerializeField] private TMP_Text gameInfo;
    public Image gameImage;

    public void OnChange()
    {
        switch (index)
        {
            case 0:
                gameInfo.text = "\"Atari Vs Badtari\" is an arcade style game which takes heavy inspiration from the game ‘Missile Command’ released by Atari in 1980.";
                break;
            
            case 1:
                gameInfo.text = "\"Mega dude\" is a game that was heavily inspired by the mega man games that were available during the early Nintendo days between 1987 and 1993. ";
                break;
            
            case 2:
                gameInfo.text = "The game \"Firewall\" gets its inspiration from the famous 1980’s maze action video game Pacman. ";
                break;
            
            case 3:
                gameInfo.text = "This game is a Sega Saturn inspired Metroidvania in which the player must guide themselves though a dangerous castle";
                break;
            
            case 4:
                gameInfo.text = "In this Crash Bandicoot inspired 3D Platformer game, players must navigate through various pitfalls and jumping puzzles.";
                break;
            
            case 5:
                gameInfo.text = "\"FROOM\" is inspired by the SNES port DOOM.";
                break;
        }
    }

    public void OnClick()
    {
        Time.timeScale = 1;
        switch (index)
        {
            case 0:
                SceneManager.LoadScene("AtariGame");
                break;
            
            case 1:
                SceneManager.LoadScene("L_MDMainMenu");
                break;
            
            case 2:
                SceneManager.LoadScene("menu");
                break;
            
            case 3:
                SceneManager.LoadScene("DoomMenu");
                break;
            
            case 4:
                SceneManager.LoadScene("MainMenu");
                break;
            
            case 5:
                SceneManager.LoadScene("L_Saturn");
                break;
        }
    }

}
