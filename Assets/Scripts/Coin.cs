using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The slot in the header that relates to this coin. ORDER MATTERS!!!")]
    GameObject coinSlot;

    Sprite coinImg;

    AudioSource coinSound;

    private void Awake()
    {
        coinImg = GetComponent<SpriteRenderer>().sprite;
        coinSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            coinSlot.GetComponent<Image>().sprite = coinImg;
            if(GetComponent<SpriteRenderer>().enabled) 
            {
                coinSound.Play();
            }  
            GetComponent<SpriteRenderer>().enabled = false;
            enabled = false;
        }
    }
}
