using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorReset : MonoBehaviour
{
    [SerializeField]
    GameObject key;

    private static readonly string closeTrigger = "ClosePortal";
    private static int closeHash = Animator.StringToHash(closeTrigger);

    public void ResetDoor()
    {
        

        if (key.GetComponent<SpriteRenderer>().enabled == false)
        {
            GetComponent<Animator>().SetTrigger(closeHash);
            key.GetComponent<SpriteRenderer>().enabled = true;
            key.GetComponent<KeyBehavior>().enabled = true;
        }
    }
}
