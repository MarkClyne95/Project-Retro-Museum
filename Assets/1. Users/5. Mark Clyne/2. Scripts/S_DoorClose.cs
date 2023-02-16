using System;
using UnityEngine;

public class S_DoorClose : MonoBehaviour
{
    [SerializeField] private GameObject _doorExit;
    [SerializeField] private IS_WizardScript _bossActivator;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) _doorExit.SetActive(true);
        Debug.Log("Close");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IS_WizardScript.instance.StartFight();
    }
}
