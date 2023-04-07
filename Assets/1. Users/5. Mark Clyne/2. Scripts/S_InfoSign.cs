using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_InfoSign : MonoBehaviour
{
    [SerializeField] private GameObject _infoUI;
    public string infoText;
    private S_InfoSign[] b2d = new S_InfoSign[20];

    private void Start()
    {
        b2d = FindObjectsOfType<S_InfoSign>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
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
        foreach (var box in b2d)
        {
            box.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
