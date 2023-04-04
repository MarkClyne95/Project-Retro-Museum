using UnityEngine;



public class SC_Barrier : MonoBehaviour
{
    [SerializeField] private GameObject[] houses;
    [SerializeField] private SC_PlayerStats stats;

    [Header("Events")]
    [SerializeField] private SC_EventManager events;

    // Start is called before the first frame update
    void Start()
    {
        events = GameObject.FindGameObjectWithTag("Player").GetComponent<SC_EventManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BadtariCart"))
        {
            Destroy(collision.gameObject);

            stats.PlayerHealth -= 1;

            events.BadtariLanded();
            houses[stats.PlayerHealth].SetActive(false);

            SC_WinLevel.instance.ScoreCheck();
        }
        else if (collision.gameObject.CompareTag("AtariCart"))
        {
            Destroy(collision.gameObject);
            
            events.AtariLanded();

            SC_WinLevel.instance.ScoreCheck();
        }
    }
}

