using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ItemPickup : MonoBehaviour
{
    private S_GameManager _gm;
    private GameObject player;
    private Animator anim;
    private bool _interacted;
    public int coinValue;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();

        anim.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !_interacted)
        {
            switch (tag)
            {
                case "Coin":
                    _gm = GameObject.FindObjectOfType<S_GameManager>();
                    Destroy(gameObject);
                    _gm.SetCoinAmount(_gm.GetCoinAmount() + coinValue);
                    _interacted = true;
                    break;
                
                case "HistoryBadge":
                    _interacted = true;
                    Debug.Log(tag);
                    break;
                
                case "SoftwareBadge":
                    _interacted = true;
                    Debug.Log(tag);
                    break;
                
                case "AffordanceBadge":
                    _interacted = true;
                    Debug.Log(tag);
                    break;
                
                case "HardwareBadge":
                    _interacted = true;
                    Debug.Log(tag);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _interacted = false;
        }
    }

    private void LateUpdate()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) < 15.0f)
        {
            anim.enabled = true;
        }
    }
}
