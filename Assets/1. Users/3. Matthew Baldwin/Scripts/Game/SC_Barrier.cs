using UnityEngine;



public class SC_Barrier : MonoBehaviour
{
    [SerializeField] private GameObject[] houses;
    [SerializeField] private SC_PlayerStats stats;

    [Header("Audio")]
    [SerializeField] private AudioSource loseHealthSFX;
    [SerializeField] private AudioSource saveCartSFX;

    // Start is called before the first frame update
    void Start()
    {

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
            stats.DecreaseScore();

            loseHealthSFX.PlayOneShot(loseHealthSFX.clip, .5f);
            houses[stats.PlayerHealth].SetActive(false);

            SC_WinLevel.instance.ScoreCheck();
        }
        else if (collision.gameObject.CompareTag("AtariCart"))
        {
            Destroy(collision.gameObject);
            saveCartSFX.PlayOneShot(saveCartSFX.clip, .5f);
            stats.IncreaseScore();

            SC_WinLevel.instance.ScoreCheck();
        }
    }
}

