using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SC_PlayerStats : MonoBehaviour
    {
        [SerializeField] private int playerHealth;
        [SerializeField] private int playerScore;

        [SerializeField] private TextMeshProUGUI scoreUI;
        

        private void Start()
        {
            scoreUI.text = "score: " + playerScore;
        }

        public int PlayerHealth
        {
            get { return playerHealth; }
            set { playerHealth = value; }
        }

        public int PlayerScore
        {
            get { return playerScore; }
            set { playerScore = value; }
        }

        public void IncreaseScore()
        {
            playerScore += 500;
            scoreUI.text = "score: " + playerScore;
        }

        public void DecreaseScore()
        {
            playerScore -= 250;
            scoreUI.text = "score: " + playerScore;
        }
    }

