using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SC_CursorControls : MonoBehaviour
{
    [Header("Game Objects")] [SerializeField]
    private Texture2D cursor;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Vector2 _mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void FixedUpdate()
    {
        _mousePosition = Input.mousePosition;
    }


    //THIS BIT MARK
    private void OnControl(InputValue ctx)
    {
        
    }

    private void OnExplosion()
    {       
        Vector3 mousePos = new Vector3(_mousePosition.x, _mousePosition.y, 11);

        Instantiate(explosion, (Vector3)Camera.main.ScreenToWorldPoint(mousePos), this.gameObject.transform.rotation);
        
    }
}
