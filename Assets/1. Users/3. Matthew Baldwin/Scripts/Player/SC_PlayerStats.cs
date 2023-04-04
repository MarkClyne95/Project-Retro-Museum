using TMPro;
using UnityEngine;


public class SC_PlayerStats : MonoBehaviour
{
    [SerializeField] private byte playerHealth;
    [SerializeField] private int playerScore;

    [SerializeField] private TextMeshProUGUI scoreUI;


    private void Start()
    {
        scoreUI.text = "score: " + playerScore;
    }

    public byte PlayerHealth
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

    public void ScoreReset()
    {
        playerScore = 0;
        scoreUI.text = "score: " + playerScore;
    }

    private void OnEnable()
    {
        SC_EventManager.OnBadtariDestroy += IncreaseScore;
        SC_EventManager.OnAtariDestroy += DecreaseScore;
        SC_EventManager.OnAtariLand += IncreaseScore;
        SC_EventManager.OnBadtariLand += DecreaseScore;
        SC_EventManager.OnNextLevel += ScoreReset;
    }

    private void OnDisable()
    {
        SC_EventManager.OnBadtariDestroy -= IncreaseScore;
        SC_EventManager.OnAtariDestroy -= DecreaseScore;
        SC_EventManager.OnAtariLand -= IncreaseScore;
        SC_EventManager.OnBadtariLand -= DecreaseScore;
        SC_EventManager.OnNextLevel += ScoreReset;
    }
}

