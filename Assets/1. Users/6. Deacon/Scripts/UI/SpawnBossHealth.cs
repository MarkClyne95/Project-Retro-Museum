using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBossHealth : MonoBehaviour
{
    public GameObject bossHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossHealth.gameObject.SetActive(true);
        }
    }

}
