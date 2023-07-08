using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoG_MoveBlock : MonoBehaviour
{
    public bool isSelected;
    [SerializeField] float moveFactor = .25f;
    // Start is called before the first frame update
    void Start()
    {
        moveFactor = .25f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSelected)
        {
            checkShift();
            
            Move();
        }
    }
    
    void checkShift()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveFactor = .1f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveFactor = .25f;
        }

    }

    void Move()
    {
        if (Input.GetKey("w"))
        {
            transform.position += Vector3.up * moveFactor;
        }
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left * moveFactor;
        }
        if (Input.GetKey("s"))
        {
            transform.position += Vector3.down * moveFactor;
        }
        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * moveFactor;
        }
    }
}
