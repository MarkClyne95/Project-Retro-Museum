using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KillCart : MonoBehaviour
{
    [SerializeField] private AudioSource atariSFX;
    [SerializeField] private AudioSource badtariSFX;
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
        if (collision.gameObject.CompareTag("AtariCart"))
        {
            Destroy(collision.gameObject);
            atariSFX = GameObject.FindGameObjectWithTag("AtariCartSFX").GetComponent<AudioSource>();
            atariSFX.PlayOneShot(atariSFX.clip, 2f);
        }

        if (collision.gameObject.CompareTag("BadtariCart"))
        {
            Destroy(collision.gameObject);
            badtariSFX = GameObject.FindGameObjectWithTag("BadtariCartSFX").GetComponent<AudioSource>();
            badtariSFX.PlayOneShot(badtariSFX.clip, 2f);
        }
    }
}
