using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ItemPickup : MonoBehaviour
{
    private S_GameManager _gm;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _gm = GameObject.FindObjectOfType<S_GameManager>();
            Destroy(gameObject);
            _gm.SetCoinAmount(_gm.GetCoinAmount() + 1);
        }
    }
}
