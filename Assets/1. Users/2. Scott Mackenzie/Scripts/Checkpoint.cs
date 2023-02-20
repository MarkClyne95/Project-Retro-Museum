using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gm;

    //This script is used to just set the player's current position as a respawn point whenever they pass through a checkpoint
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Set");
            Invoke(nameof(SetNewCheckpoint), 1f); //Delay added as a safety measure
        }
    }

    private void SetNewCheckpoint()
    {
        gm.lastCheckpointPos = transform.position;
        Destroy(this.gameObject);
    }

}
