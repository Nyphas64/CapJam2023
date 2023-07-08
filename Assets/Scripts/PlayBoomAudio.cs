using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBoomAudio : MonoBehaviour
{
    public AudioClip soundClip; // Assign the sound clip in the Unity editor

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Debug.Log("Audio error on boom");
        }
    }
    
    void Awake()
    {
        if (soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
        else
        {
            Debug.Log("Audio error on boom");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
