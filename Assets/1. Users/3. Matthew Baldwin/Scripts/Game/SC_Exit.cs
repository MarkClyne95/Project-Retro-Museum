using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Exit : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("AtariExplosion"))
        {
            Application.Quit();
            Debug.Log("Have Quit");
        }
    }
}
