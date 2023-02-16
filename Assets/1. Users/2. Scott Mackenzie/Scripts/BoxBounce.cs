using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBounce : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Bounce");
            Invoke("playerBounce", 0.1f);
        }
    }

    private void playerBounce()
    {
        player.GetComponent<ThirdPersonLocomotion>().Jump();
        player.GetComponent<Animator>().SetBool("IsJumping", true);
    }
}
