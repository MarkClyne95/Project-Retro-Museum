using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Exit : MonoBehaviour
{
    public Color color;
    public int speed;
    public string toLoad;
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
            Initiate.Fade(toLoad, color, speed);
        }
    }
}
