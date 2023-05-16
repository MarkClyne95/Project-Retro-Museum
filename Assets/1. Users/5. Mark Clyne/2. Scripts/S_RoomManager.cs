using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RoomManager : MonoBehaviour
{
    public static S_RoomManager obj;
    public GameObject virtualCam;
    private void Awake()
    {
        obj = this;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            virtualCam.SetActive(false)
;
        }
    }
}

