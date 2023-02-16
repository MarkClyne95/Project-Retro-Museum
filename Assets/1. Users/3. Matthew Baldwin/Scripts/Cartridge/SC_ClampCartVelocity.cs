using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ClampCartVelocity : MonoBehaviour
{
    [SerializeField] private float maxVelocity;
    [SerializeField] private Rigidbody2D physics;
    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        physics.velocity = Vector2.ClampMagnitude(physics.velocity, maxVelocity);
    }
}
