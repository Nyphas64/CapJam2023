using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorReset : MonoBehaviour
{
    [SerializeField]
    GameObject key;

    private static readonly string closeTrigger = "ClosePortal";
    private static int closeHash = Animator.StringToHash(closeTrigger);

    public void StartResetDoor()
    {
        StartCoroutine(ResetDoor());
    }

    public IEnumerator ResetDoor()
    {
        

        if (key.GetComponent<SpriteRenderer>().enabled == false)
        {
            GetComponent<Animator>().SetTrigger(closeHash);
            yield return new WaitForSeconds(3);
            key.GetComponent<SpriteRenderer>().enabled = true;
            key.GetComponent<KeyBehavior>().enabled = true;
        }
    }
}
