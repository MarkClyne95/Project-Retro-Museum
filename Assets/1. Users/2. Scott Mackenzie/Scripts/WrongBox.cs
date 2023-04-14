using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongBox : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private GameObject crateSmoke;

    [SerializeField] private GameObject crateMesh;

    [SerializeField] private AudioSource crateDestruction;

    [SerializeField] private Collider collider1;
    [SerializeField] private Collider collider2;

    private void Start()
    {
        crateDestruction.volume = PlayerPrefs.GetFloat("SFXVol");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Disable Box");
            disableBox();
        }
    }

    private void disableBox()
    {
        //Play smoke VFX
        Instantiate(crateSmoke, transform.position, transform.rotation);

        crateDestruction.Play();

        crateMesh.SetActive(false);

        collider1.enabled = false;
        collider2.enabled = false;

    }


}
