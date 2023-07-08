using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorBehavior : MonoBehaviour
{

    private static readonly string closeTrigger = "ClosePortal";
    private int closeHash = Animator.StringToHash(closeTrigger);
    
    public void ActivateCollider()
    {
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetTrigger(closeHash);
        }
    }
}
