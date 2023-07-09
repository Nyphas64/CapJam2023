using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPrinter : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 velocity = rb.velocity;
        Debug.Log("Velocity: " + velocity.magnitude);
    }
}
