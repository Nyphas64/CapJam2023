using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoG_MoveBlock : MonoBehaviour
{
    public bool isSelected;
<<<<<<< Updated upstream
    [SerializeField] float moveFactor = .05f;
    // Start is called before the first frame update
    void Start()
    {
        
=======
    [SerializeField] float moveFactor;
    // Start is called before the first frame update
    void Start()
    {
        moveFactor = .15f;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSelected)
        {
            Move();
        }
    }
<<<<<<< Updated upstream
=======
    

    void checkShift()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveFactor = .075f;
        }
        else
        {
            moveFactor = .15f;
        }

    }
>>>>>>> Stashed changes

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
