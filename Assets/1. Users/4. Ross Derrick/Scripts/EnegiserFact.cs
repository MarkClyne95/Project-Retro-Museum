using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnegiserFact : MonoBehaviour{
    [SerializeField] private GameObject factCanvas;
    private bool checkInput = false;

    private void Update() {
        if (checkInput && Input.GetKeyDown(KeyCode.Space)) {
            CloseFact();
        }
    }

    public void OpenFact() {
        factCanvas.SetActive(true);
        Time.timeScale = 0;
        checkInput = true;

    }

    public void CloseFact() {
        factCanvas.SetActive(false);
        Time.timeScale = 1;
        checkInput = false;
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "pacman")
        {
            OpenFact();
            //Destroy(gameObject);
        }
    }
    
}
