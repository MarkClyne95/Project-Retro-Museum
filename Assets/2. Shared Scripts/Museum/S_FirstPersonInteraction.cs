using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_FirstPersonInteraction : MonoBehaviour
{
    public bool canInteract;
    public KeyCode interactKey = KeyCode.E;
    [SerializeField]private float _rayLength = 50f;
    public TMP_Text interactText;
    [SerializeField] private S_QuestionHandler questionUI;
    public S_InteractableObject obj;

    private void Start()
    {
        QualitySettings.SetQualityLevel(5);
        Screen.SetResolution(1920, 1080, true);
        interactText.text = $"Press {interactKey} to interact";
    }

    private void Update()
    {
            if (obj != null)
            {
                interactText.gameObject.SetActive(true);
                switch (obj.objectType)
                {
                    case ObjectType.Console:
                        Debug.Log("Console");
                        HandleConsole(obj.levelName);
                        break;

                    case ObjectType.Door:
                        HandleDoor(obj);
                        break;
                }
            }
        
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

    public void HandleConsole(string levelName)
    {
        if (Input.GetKey(interactKey) && canInteract)
        {
            SceneManager.LoadScene(levelName);
        }
    }

    public void HandleDoor(S_InteractableObject obj)
    {
        if (Input.GetKey(interactKey) && canInteract && !obj.questionAnswered)
        {
            questionUI.gameObject.SetActive(true);
        }
    }
}
