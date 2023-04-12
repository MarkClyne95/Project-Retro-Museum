using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class S_GameManager : MonoBehaviour
{
    [SerializeField]private int _coinAmount;
    [SerializeField] private GameObject affordanceBadge;
    [SerializeField] private TMP_Text coinText;

    public static S_GameManager instance;

    private void Start()
    {
        instance = this;
        QualitySettings.SetQualityLevel(6);
        Screen.SetResolution(800,600,true);
    }

    public S_GameManager()
    {
        _coinAmount = 0;
    }

    public void SetCoinAmount(int value)
    {
        _coinAmount = value;
        coinText.text = _coinAmount.ToString();
        
        if (GetCoinAmount() >= 90)
        {
            affordanceBadge.SetActive(true);
        }
    }

    public int GetCoinAmount()
    {
        return _coinAmount;
    }
}

[Flags]
public enum Badges
{
    Hardware = 0x000001,
    Software = 0x000010,
    History = 0x000011,
    Affordance = 0x000100
}
