using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_InfoSign : MonoBehaviour
{
    [SerializeField] private GameObject _infoUI;
    public string infoText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            S_MetroidVaniaPlayerController.instance.canMove = false;
            _infoUI.SetActive(true);
            _infoUI.gameObject.GetComponentInChildren<TMP_Text>().text = infoText;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Unpause()
    {
        Invoke(nameof(ResetCollider), 2.0f);
        _infoUI.SetActive(false);
        Time.timeScale = 1;
        S_MetroidVaniaPlayerController.instance.canMove = true;
    }

    private void ResetCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
