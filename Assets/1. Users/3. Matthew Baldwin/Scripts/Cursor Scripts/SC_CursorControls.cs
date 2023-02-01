using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SC_CursorControls : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    public Vector3 uh = new Vector3();
    public Vector3 eh = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.transform.position = (Vector3)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        uh = (Vector3)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        uh.z = 0;
        this.gameObject.transform.position = uh;
    
    }

    private void OnControl(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }
}
