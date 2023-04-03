using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_DeathZone : MonoBehaviour
{
    [SerializeField]private S_GameFade _fade;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _fade.FadeOutAndIn();
        }
    }
}
