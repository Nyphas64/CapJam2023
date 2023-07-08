using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject character;

    [SerializeField]
    float size;

    [SerializeField]
    float timeToMove;

    float width;
    float widthThird;
    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Camera.main.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, Camera.main.transform.position.z);
        Camera.main.orthographicSize = size;

        width = Camera.main.aspect * Camera.main.orthographicSize;

    }

    private void Start()
    {
        widthThird = (width*2)/ 3;
    }

    private void Update()
    {
        if(character.transform.position.x < Camera.main.transform.position.x - width + widthThird || character.transform.position.x > Camera.main.transform.position.x + width - widthThird)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(character.transform.position);
            Vector3 delta = character.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0.5f,point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, timeToMove);
        }
       
    }
    
}

