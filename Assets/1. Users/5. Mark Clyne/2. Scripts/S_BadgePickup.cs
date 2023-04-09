using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_BadgePickup : MonoBehaviour
{
    [SerializeField] private BadgeType _badgeType;
    [SerializeField] private GameObject _playerUI;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            switch (_badgeType)
            {
                case BadgeType.Affordance:
                    _playerUI.GetComponent<S_PlayerUI>().SetAffordanceBadge(gameObject.GetComponent<S_BadgePickup>());
                    Destroy(gameObject);
                    break;
            
                case BadgeType.Hardware:
                    _playerUI.GetComponent<S_PlayerUI>().SetHardwareBadge(gameObject.GetComponent<S_BadgePickup>());
                    Destroy(gameObject);
                    break;
            
                case BadgeType.History:
                    _playerUI.GetComponent<S_PlayerUI>().SetHistoryBadge(gameObject.GetComponent<S_BadgePickup>());
                    Destroy(gameObject);
                    break;
            
                case BadgeType.Software:
                    _playerUI.GetComponent<S_PlayerUI>().SetSoftwareBadge(gameObject.GetComponent<S_BadgePickup>());
                    Destroy(gameObject);
                    break;
            
                case BadgeType.None:
                    break;
            }
        }
    }
}

public enum BadgeType
{
    None,
    History,
    Hardware,
    Software,
    Affordance
}
