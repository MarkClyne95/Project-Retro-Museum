using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxDestruction : MonoBehaviour
{
    [SerializeField] private GameObject crateSmoke;

    [SerializeField] private GameObject crateMesh;

    [SerializeField] private GameObject retroUI;

    private void Start()
    {
        retroUI.SetActive(false);
    }

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
        //Play smoke VFX
        Instantiate(crateSmoke, transform.position, transform.rotation);

        //Show specific retro game fact
        retroUI.SetActive(true);

        //Then hide self
        crateMesh.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().detectCollisions = false;
    }
}
