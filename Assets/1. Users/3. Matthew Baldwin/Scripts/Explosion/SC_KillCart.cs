using UnityEngine;



public class SC_KillCart : MonoBehaviour
{
    [SerializeField] private AudioSource atariSFX;
    [SerializeField] private AudioSource badtariSFX;
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
        if (collision.gameObject.CompareTag("AtariCart"))
        {
            Destroy(collision.gameObject);
            events.AtariKilled();
        }

        if (collision.gameObject.CompareTag("BadtariCart"))
        {
            Destroy(collision.gameObject);
            events.BadtariKilled();
        }
    }
}
