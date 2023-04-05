using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class S_DeathZone : MonoBehaviour
{
    private UnityEvent uEvent;
    [SerializeField]private S_GameFade _fade;

    private void Start()
    {
        if (uEvent == null)
        {
            uEvent = new UnityEvent();
        }
        
        uEvent.AddListener(Death);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uEvent?.Invoke();
        }
    }

    private void Death()
    {
        S_MetroidVaniaPlayerController.instance.transform.position = new Vector2(501, 309);
    }
}
