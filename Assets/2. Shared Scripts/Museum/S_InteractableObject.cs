using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class S_InteractableObject : MonoBehaviour
{
    public ObjectType objectType;
    public string levelName;
    [TextArea(15, 20)]
    [CanBeNull] public string historyInformation;
}

public enum ObjectType
{
    Console, 
    History,
    Door
}
