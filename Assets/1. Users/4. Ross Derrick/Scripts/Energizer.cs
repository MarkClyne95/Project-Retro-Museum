﻿using UnityEngine;
using System.Collections;

public class Energizer : MonoBehaviour {

    private Firewall.GameManager gm;

	// Use this for initialization
	void Start ()
	{
	    gm = GameObject.Find("Game Manager").GetComponent<Firewall.GameManager>();
        if( gm == null )    Debug.Log("Energizer did not find Game Manager!");
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "pacman")
        {
            gm.ScareGhosts();
            Destroy(gameObject);
        }
    }
}
