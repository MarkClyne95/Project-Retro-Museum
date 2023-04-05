using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EndlessActivate : MonoBehaviour
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<SC_PlayerStats>().endless = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SC_SwitchLevel>().NextScene();
        }
    }
}
