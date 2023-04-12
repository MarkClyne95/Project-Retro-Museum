using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroidSpawnRespawner : MonoBehaviour
{
    public GameObject respawn1;
    public GameObject respawn2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            respawn2.SetActive(true);
            respawn1.SetActive(false);
        }
    }
}
