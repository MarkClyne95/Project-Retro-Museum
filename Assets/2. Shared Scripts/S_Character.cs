using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Character : MonoBehaviour
{
    public string characterName;
    public int healthPoints;
    public int maxHealthPoints = 10;
    public float energyPoints;

    private void Start()
    {
        characterName = gameObject.name;
    }
}