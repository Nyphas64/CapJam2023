using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherPlatform : MonoBehaviour
{
    [SerializeField]
    float launchStrength;

    

    [SerializeField]
    float moveSpeed;

    float rotation = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            LaunchPlayer(collision.gameObject);
        }
    }

    private void Update()
    {
        var rot = Input.GetAxis("Horizontal");
        rotation += rot * moveSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0,0,rotation);
      
    }

    void LaunchPlayer(GameObject player)
    {
        player.transform.position = transform.position;
        
        player.GetComponent<Rigidbody2D>().AddForce(transform.forward * launchStrength);
    }

}
