using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoomTrigger : MonoBehaviour
{
    public ParticleSystem particlePrefab;   
    public AudioClip soundClip;            

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //@brady, idk if youre looking for player tag or block tag or wha
        {
            ActivateParticleSystem();
            PlaySound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //balls
    }

    private void ActivateParticleSystem()
    {
        if (particlePrefab != null)
        {
            ParticleSystem newParticleSystem = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            newParticleSystem.Play();
            Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration);
        }
        else
        {
            Debug.Log("particle system loading error, could not be loaded successfully x_x");
        }
    }
       
    private void PlaySound()
    {
        if (soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}
