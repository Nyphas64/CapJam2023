using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFuckerUpper : MonoBehaviour
{
    //I fucking swear if the earth wasn't flat we wouldn't have these kinds of problems stupid fucking sphere earth-believing mongoloids and their stupid fucking game engines
    
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("colliding OwO");
        if (other.CompareTag("Block"))
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0f;
            Debug.Log("touching OwO");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("no longer colliding UwU");
        if (other.CompareTag("Block"))
        {
            rb.gravityScale = 1f;
            Debug.Log("no longer touching UwU");
        }
    }
}
