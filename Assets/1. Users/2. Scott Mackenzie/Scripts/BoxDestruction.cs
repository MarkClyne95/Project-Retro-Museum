using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxDestruction : MonoBehaviour
{
    [SerializeField] private GameObject crateSmoke;

    [SerializeField] private GameObject crateMesh;

    [SerializeField] private GameObject retroUI;

    [SerializeField] private AudioSource crateDestruction;

    private void Start()
    {
        retroUI.SetActive(false);
        crateDestruction.volume = PlayerPrefs.GetFloat("SFXVol");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Break");

            GetComponent<Animator>().SetBool("SquashStretch", true);
            Invoke("Splinter", 0.3f);
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           GetComponent<Animator>().SetBool("SquashStretch", false);
        }

    }

    private void Splinter()
    {
        //Play smoke VFX
        Instantiate(crateSmoke, transform.position, transform.rotation);

        crateDestruction.Play();

        //Show specific retro game fact
        retroUI.SetActive(true);

        //Then hide self
        crateMesh.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().detectCollisions = false;
    }
}
