using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private static readonly string doorTrigger = "OpenPortal";
    private int doorHash = Animator.StringToHash(doorTrigger);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            door.GetComponent<Animator>().SetTrigger(doorHash);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            enabled = false;
        }
    }
}
