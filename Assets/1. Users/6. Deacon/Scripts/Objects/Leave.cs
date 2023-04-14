using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leave : MonoBehaviour
{
    public GameObject door;
    public buttonManager ButtonManager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene("L_90sFloor");
        }
    }

}
