using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            if (damageable != null)
            {
                MetroidScoring.totalScore += 1;
                damageable.Hit(damage: 10, knockback: Vector2.zero); // Adjust the damage value as needed
            }
        }
    }
}
