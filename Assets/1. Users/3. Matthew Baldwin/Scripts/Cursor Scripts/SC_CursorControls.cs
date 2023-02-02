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

    [SerializeField] private Texture2D explosion;
    private Vector2 _mousePosition;

    public Vector3 uh = new Vector3();

    public Vector3 eh = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //THIS BIT MARK
    private void OnControl(InputValue ctx)
    {
        
    }

    private void OnExplosion()
    {
        Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }


}
