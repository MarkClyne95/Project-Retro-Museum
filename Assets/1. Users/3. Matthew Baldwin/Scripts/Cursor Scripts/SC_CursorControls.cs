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
    [SerializeField] private Vector3 mousePos;

    [Header("Turret Elements")]
    [SerializeField] private SpriteRenderer turret;
    [SerializeField] private Sprite currentTurretSprite;
    [SerializeField] private Sprite[] turretOrientation;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        currentTurretSprite = turret.sprite;
        currentTurretSprite = turretOrientation[0];
    }


    private void OnExplosion()
    {       
        mousePos = new Vector3(Input.mousePosition.x + 16, Input.mousePosition.y - 16, 11);

        Instantiate(explosion, (Vector3)Camera.main.ScreenToWorldPoint(mousePos), this.gameObject.transform.rotation);


        switch (mousePos.x)
        {
            case <= 300:
                currentTurretSprite = turretOrientation[1];
                turret.sprite = currentTurretSprite;
                break;
            case >= 400:
                currentTurretSprite = turretOrientation[2];
                turret.sprite = currentTurretSprite;
                break;
            default:
                currentTurretSprite = turretOrientation[0];
                turret.sprite = currentTurretSprite;
                break;
        }
    }
}
