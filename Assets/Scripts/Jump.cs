using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour, IAction
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAction()
    {
        
    }
}
