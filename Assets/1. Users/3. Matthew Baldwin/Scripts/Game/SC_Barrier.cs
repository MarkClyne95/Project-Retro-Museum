using UnityEngine;



public class SC_Barrier : MonoBehaviour
{
    [SerializeField] private GameObject[] houses;
    [SerializeField] private SC_PlayerStats stats;
    [SerializeField] private SC_SwitchLevel switchLevel;

    [Header("Events")]
    [SerializeField] private SC_EventManager events;

    // Start is called before the first frame update
    void Start()
    {
        events = GameObject.FindGameObjectWithTag("Player").GetComponent<SC_EventManager>();
        switchLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<SC_SwitchLevel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BadtariCart"))
        {
            Destroy(collision.gameObject);

            if (stats.PlayerHealth > 1)
            {
                stats.PlayerHealth -= 1;
                houses[stats.PlayerHealth].SetActive(false);
                events.BadtariLanded();

                SC_WinLevel.instance.ScoreCheck();
            }
            else
            {
                events.EndGame();
                switchLevel.Invoke("FullReset", 3f);
            }
        }
        else if (collision.gameObject.CompareTag("AtariCart"))
        {
            Destroy(collision.gameObject);
            
            events.AtariLanded();

            SC_WinLevel.instance.ScoreCheck();
        }
    }

    private void ResetHouses()
    {
        foreach (GameObject g in houses)
        {
            g.SetActive(true);
        }
    }

    private void OnEnable()
    {
        SC_EventManager.OnGameOver += ResetHouses;
        SC_EventManager.OnNextLevel += ResetHouses;
    }

    private void OnDisable()
    {
        SC_EventManager.OnGameOver -= ResetHouses;
        SC_EventManager.OnNextLevel -= ResetHouses;
    }

}

