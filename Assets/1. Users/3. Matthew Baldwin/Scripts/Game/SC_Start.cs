using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Start : MonoBehaviour
{
    [SerializeField] private SC_SwitchLevel level;
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
            level.NextScene();
        }
    }
}
