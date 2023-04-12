using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBounce : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private GameObject crateSmoke;

    [SerializeField] private GameObject crateMesh;

    [SerializeField] private int counter = 0;

    [SerializeField] private AudioSource crateBounce;
    [SerializeField] private AudioSource crateDestruction;

    [SerializeField] private Collider collider1;
    [SerializeField] private Collider collider2;

    private void Start()
    {
        crateBounce.volume = PlayerPrefs.GetFloat("SFXVol");
        crateDestruction.volume = PlayerPrefs.GetFloat("SFXVol");
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetBool("SquashStretch", true);

        if (other.CompareTag("Player") && player.GetComponent<ThirdPersonLocomotion>().isGrounded)
        {
            Debug.Log("Bounce");

            crateBounce.Play();
            Invoke("playerBounce", 0.01f);
        }
    }

    private void playerBounce()
    {
        player.GetComponent<ThirdPersonLocomotion>().jumpHeight = 4f;
        player.GetComponent<ThirdPersonLocomotion>().Jump();
        player.GetComponent<Animator>().SetBool("IsJumping", true);

        //Destroy crate after player jumps on it a certain number of times
        //counter += 1;

        //if(counter >= 3)
        //{
        //    //Play smoke VFX
        //    Instantiate(crateSmoke, transform.position, transform.rotation);

        //    crateDestruction.Play();

        //    crateMesh.SetActive(false);

        //    collider1.enabled = false;
        //    collider2.enabled = false;
        //}
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Back to Crate Idle");
            GetComponent<Animator>().SetBool("SquashStretch", false);
        }
    }

}
