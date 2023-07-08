using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject screenToLoad;

    private static readonly string playerTrigger = "EndLevel";
    private int playerHash = Animator.StringToHash(playerTrigger);

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
            collision.gameObject.GetComponent<Animator>().SetTrigger(playerHash);
            StartCoroutine(CloseDoorAnim(collision.gameObject));
        }
    }

    IEnumerator CloseDoorAnim(GameObject player)
    {
        
        yield return new WaitForSeconds(1f);
        player.SetActive(false);
        gameObject.GetComponent<Animator>().SetTrigger(closeHash);
        yield return new WaitForSeconds(3f);
        screenToLoad.SetActive(true);

    }    
}
