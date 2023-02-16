using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestruction : MonoBehaviour
{
    public GameObject crateSmoke;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Break");
            Invoke("Splinter", 0.3f);
            
        }
    }

    private void Splinter()
    {
        Instantiate(crateSmoke, transform.position, transform.rotation);

        //Show specific retro game fact

        //Then destroy self
        Destroy(gameObject);
    }
}
