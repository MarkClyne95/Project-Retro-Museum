using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public static PlayerStart Instance;

    //private void Awake()
    //{
    //    if (Instance == null) // If there is no instance already
    //    {
    //        DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
    //        Instance = this;
    //    }
    //    else if (Instance != this) // If there is already an instance and it's not `this` instance
    //    {
    //        Destroy(gameObject); // Destroy the GameObject, this component is attached to
    //    }
    //}
}
