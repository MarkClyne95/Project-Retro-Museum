using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameManager : MonoBehaviour
{
    [SerializeField]private int _coinAmount;
    [SerializeField]private string[] _badges;

    public static S_GameManager instance;

    public S_GameManager()
    {
        _coinAmount = 0;
        _badges = null;
    }

    public void SetCoinAmount(int value)
    {
        _coinAmount = value;
    }

    public int GetCoinAmount()
    {
        return _coinAmount;
    }

    public void SetBadges(string[] value)
    {
        _badges = value;
    }

    public string[] GetBadges()
    {
        return _badges;
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
