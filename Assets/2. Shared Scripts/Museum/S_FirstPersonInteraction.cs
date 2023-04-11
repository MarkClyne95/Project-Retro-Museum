using System;
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
                
                    case ObjectType.History:
                        HandleHistory();
                        break;
                
                    case ObjectType.Door:
                        HandleDoor();
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

    private void HandleHistory()
    {
        if (Input.GetKey(interactKey) && canInteract)
        {
            //TODO: Show history UI
        }
    }

    private void HandleDoor()
    {
        if (Input.GetKey(interactKey) && canInteract)
        {
            //TODO: Open door if its 
        }
    }
}
