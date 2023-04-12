﻿using UnityEngine;
using System.Collections;

namespace Firewall{
	public class Pacdot : MonoBehaviour{

		void OnTriggerEnter2D(Collider2D other) {
			if (other.name == "pacman") {
				GameManager.score += 10;
				GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");
				Destroy(gameObject);

				if (pacdots.Length == 1) {
					FindObjectOfType<GameGUINavigation>().LoadLevel();
				}
			}
		}
	}
}
