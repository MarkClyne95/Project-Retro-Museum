using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerStats : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    [SerializeField] private int playerScore;

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
}
