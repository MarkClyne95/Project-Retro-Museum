using UnityEngine;

public class S_DoorClose : MonoBehaviour
{
    [SerializeField] private GameObject _doorExit;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) _doorExit.SetActive(true);
        Debug.Log("Close");
    }
}
