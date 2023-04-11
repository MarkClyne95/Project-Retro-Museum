using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MetroidInteractable : MonoBehaviour
{
    public static bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            collision.gameObject.GetComponent<MetroidPlayerController>().NotifyPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            collision.gameObject.GetComponent<MetroidPlayerController>().DeNotifyPlayer();
        }
    }
}