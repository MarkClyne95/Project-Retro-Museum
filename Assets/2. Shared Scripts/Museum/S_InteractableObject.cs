using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class S_InteractableObject : MonoBehaviour
{
    public ObjectType objectType;
    public string levelName;
    public bool interactable;
    public bool questionAnswered;
    [TextArea(15, 20)]
    public string historyInformation;

    public bool consoleValue, historyValue, doorValue;
}

public enum ObjectType
{
    Console, 
    History,
    Door
}

[CustomEditor(typeof(S_InteractableObject)), CanEditMultipleObjects]
public class InteractableObjectEditor : Editor
{
    public SerializedProperty objectTypeProp, levelNameProp, interactableProp, questionAnsweredProp, historyInformationProp, consoleValueProp, historyValueProp, doorValueProp;

    private void OnEnable()
    {
        objectTypeProp = serializedObject.FindProperty("objectType");
        levelNameProp = serializedObject.FindProperty("levelName");
        interactableProp = serializedObject.FindProperty("interactable");
        questionAnsweredProp = serializedObject.FindProperty("questionAnswered");
        historyInformationProp = serializedObject.FindProperty("historyInformation");
        consoleValueProp = serializedObject.FindProperty("consoleValue");
        historyValueProp = serializedObject.FindProperty("historyValue");
        doorValueProp = serializedObject.FindProperty("doorValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(objectTypeProp);

        ObjectType ot = (ObjectType)objectTypeProp.enumValueIndex;

        switch (ot)
        {
            case ObjectType.Console:
                EditorGUILayout.PropertyField(levelNameProp, new GUIContent("Level Name"));
                EditorGUILayout.PropertyField(interactableProp, new GUIContent("Interactable"));
                break;
            
            case ObjectType.Door:
                EditorGUILayout.PropertyField(levelNameProp, new GUIContent("Level Name"));
                EditorGUILayout.PropertyField(interactableProp, new GUIContent("Interactable"));
                EditorGUILayout.PropertyField(questionAnsweredProp, new GUIContent("Question Answered"));
                break;
            
            case ObjectType.History:
                EditorGUILayout.PropertyField(interactableProp, new GUIContent("Interactable"));
                EditorGUILayout.PropertyField(historyInformationProp, new GUIContent("History Information"));
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }
}
