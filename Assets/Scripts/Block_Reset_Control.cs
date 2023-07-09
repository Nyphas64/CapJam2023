using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Reset_Control : MonoBehaviour
{

    public ParticleSystem particlePrefab;   
    public ParticleSystem whiteSmoke;  

    private Transform[] children;
    private Vector3[] initialChildPositions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //store initial child positions
        children = new Transform[transform.childCount];
        initialChildPositions = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
            initialChildPositions[i] = children[i].position;
        }

    }


    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("r is being pressed!");
            ResetBlocks();
        }
        */
        
    }

    public void ResetBlocks()
    {
        for (int i = 0; i < children.Length; i++)
        {
            //spawn explosion on child 
            // Israel/Palestine moment :flushed: 
            if (particlePrefab != null)
            {
                ParticleSystem newParticleSystem = Instantiate(particlePrefab, children[i].position, Quaternion.identity);
                newParticleSystem.Play();
                Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration); 
            }

            // Reset child object to its initial position
            children[i].position = initialChildPositions[i];

            if (whiteSmoke != null)
            {
                ParticleSystem newParticleSystem = Instantiate(whiteSmoke, children[i].position, Quaternion.identity);
                newParticleSystem.Play();
                Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration); 
            }


        }
    }
}
