using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_InteractableObject : MonoBehaviour
{
    public ObjectType objectType;
    public string levelName;
    public bool interactable;
    public bool questionAnswered;
    [TextArea(15, 20)]
    public string historyInformation;

    public bool consoleValue, historyValue, doorValue;

    private void Update()
    {
        if (questionAnswered)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}

public enum ObjectType
{
    Console, 
    History,
    Door
}
