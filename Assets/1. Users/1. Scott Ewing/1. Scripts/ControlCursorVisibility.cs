using UnityEngine;

public class ControlCursorVisibility : MonoBehaviour
{
    [SerializeField] private bool _showCursorOnStart;

    void Start()
    {
        if (_showCursorOnStart)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
