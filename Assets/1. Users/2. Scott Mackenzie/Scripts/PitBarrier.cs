using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PitBarrier : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    private GameManager gm;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Plummet player
            player.GetComponent<ThirdPersonLocomotion>().gravity = -100f;
            player.GetComponent<ThirdPersonLocomotion>().plummet();

            //Fade screen to black

            //Add camera shake after a delay to imitate player hitting the ground
            Invoke("ActivateCameraShake", 0.6f);

            //Load scene from last checkpoint
            Invoke("LoadCheckpoint", 1.5f);
        }
    }

    private void ActivateCameraShake()
    {
        CameraShakeManager.instance.CameraShake(impulseSource);
    }

    private void LoadCheckpoint()
    {
        gm.OnPlayerDeath();
    }
}
