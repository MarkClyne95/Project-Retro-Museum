using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_FirstPersonInteraction : MonoBehaviour
{
    public bool canInteract;
    public KeyCode interactKey = KeyCode.E;
    private RaycastHit _hit;
    private Vector3 _forward;
    [SerializeField]private float _rayLength = 50f;
    [SerializeField] private TMP_Text interactText;

    private void Start()
    {
        QualitySettings.SetQualityLevel(5);
        Screen.SetResolution(1920, 1080, true);
        interactText.text = $"Press {interactKey} to interact";
    }

    private void Update()
    {
        _forward = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
        Debug.DrawRay(_forward, Camera.main.transform.forward * _rayLength, Color.blue);
        if (Physics.Raycast(_forward, Camera.main.transform.forward, out _hit, _rayLength))
        {
            var interactableObject = _hit.collider.gameObject.GetComponent<S_InteractableObject>();

            if (interactableObject != null)
            {
                interactText.gameObject.SetActive(true);
                switch (interactableObject.objectType)
                {
                    case ObjectType.Console:
                        Debug.Log("Console");
                        HandleConsole(interactableObject.levelName);
                        break;
                
                    case ObjectType.Door:
                        HandleDoor(interactableObject);
                        break;
                }
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

    private void HandleConsole(string levelName)
    {
        if (Input.GetKey(interactKey) && canInteract)
        {
            SceneManager.LoadScene(levelName);
        }
    }

    private void HandleDoor(S_InteractableObject obj)
    {
        if (Input.GetKey(interactKey) && canInteract && !obj.questionAnswered)
        {
            SceneManager.LoadScene(obj.levelName);
        }

        // if (Input.GetKey(interactKey) && obj.questionAnswered)
        // {
        //     
        // }
    }
}
