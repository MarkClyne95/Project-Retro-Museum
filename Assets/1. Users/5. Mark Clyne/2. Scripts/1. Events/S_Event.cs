using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class S_Event : ScriptableObject
{
    [TextArea]
    public string finalText;
}

