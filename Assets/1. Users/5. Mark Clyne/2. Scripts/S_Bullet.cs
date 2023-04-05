using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class S_Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        S_Enemy enemy = col.GetComponent<S_Enemy>();

        if (enemy != null && !col.CompareTag("Player"))
        {
            enemy.TakeDamage(1);
            Destroy(gameObject);
        }
            
    }
}
