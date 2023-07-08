using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReset : MonoBehaviour
{
    [SerializeField]
    GameObject respawnLight;
    private static readonly string openLightTrigger = "OpenLight";
    private int openLightHash = Animator.StringToHash(openLightTrigger);

    private static readonly string closeLightTrigger = "CloseLight";
    private int closeLightHash = Animator.StringToHash(closeLightTrigger);

    private static readonly string playerDeath = "IsDying";
    private int deathHash = Animator.StringToHash(playerDeath);

    private static readonly string playerRespawn = "Respawning";
    private int respawnHash = Animator.StringToHash(playerRespawn);

    private Vector2 orgPostion;

    private void Awake()
    {
        orgPostion = transform.position;
    }

    public void TriggerDeath()
    {
        gameObject.GetComponent<Animator>().SetBool(deathHash, true);
        StartCoroutine(Respawn());
        
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Animator>().SetBool(deathHash, false);
        respawnLight.GetComponent<Animator>().SetTrigger(openLightHash);
        yield return new WaitForSeconds(1f);
        gameObject.transform.position = orgPostion;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1f);
        respawnLight.GetComponent <Animator>().SetTrigger(closeLightHash);
    }


}
