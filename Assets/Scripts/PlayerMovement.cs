using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    List<ActionObject> actions;

    ActionObject currentState;
    Queue<ActionObject> actionsQueue;

    [Header("Player Settings")]
    [SerializeField]
    int jumpHeight;
    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb;
    Vector3 moveEndPosition;

    bool isJumping = false, isMoving = false, isFinished = false, hasHitWall = false;

    float moveStartTime;
    

    public void SwitchState()
    {
        if(actionsQueue.Any())
        {
            currentState = actionsQueue.Dequeue();
        }
        else
        {
            isFinished = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        actionsQueue = new Queue<ActionObject>(actions);
        SwitchState();

    }

    private void Update()
    {
        if(!isFinished)
        {
            HandleMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasHitWall = true;
    }

    void HandleMovement()
    {
        if (currentState.action == ActionObject.Action.Jump)
        {
            Jump();
        }
        if (currentState.action == ActionObject.Action.Move)
        {
            Move();
        }
        if (currentState.action == ActionObject.Action.MoveToWall)
        {
            MoveToWall();
        }
    }


    void Jump()
    {
        rb.AddForce(new Vector2(currentState.value, 0), ForceMode2D.Force);
        if (!isJumping)
        {
            isJumping = true;
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if(rb.velocity.y == 0)
        {
            isJumping = false;
            SwitchState();
        }
    }

    void Move()
    {
        if (!isMoving)
        {
            moveEndPosition = new Vector3(transform.position.x + currentState.value, transform.position.y, transform.position.z);
            isMoving = true;
        }
        float distCovered = (Time.deltaTime - moveStartTime) * moveSpeed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / currentState.value;

        transform.position = Vector3.Lerp(transform.position, moveEndPosition, Math.Abs(fractionOfJourney));

        if (hasHitWall || Math.Abs(transform.position.x - moveEndPosition.x) <= 0.25)
        {
            isMoving= false;
            SwitchState();
        }
    }

    void MoveToWall()
    {
        if(hasHitWall)
        {
            transform.position += (new Vector3(-1 * currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
            SwitchState();
            hasHitWall= false;
            return;
        }
        transform.position += (new Vector3(currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
    }
}
