using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class S_Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rb;
    private S_Enemy enemy;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (S_MetroidVaniaPlayerController.instance._isLookingLeft)
        {
            _rb.velocity = Vector2.left * speed;
        }
        else
        {
            _rb.velocity = Vector2.right * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        enemy = col.GetComponent<S_Enemy>();

        if (enemy != null && !col.CompareTag("Player"))
        {
            enemy.TakeDamage(1);
            Destroy(gameObject);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 1);
        }
            
    }
}
