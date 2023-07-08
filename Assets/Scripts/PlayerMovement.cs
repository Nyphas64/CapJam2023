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

    bool isJumping = false, isMoving = false, isFinished = false, hasHitWall = false, isOnGround = true;

    float moveStartTime;

    [SerializeField]
    Collider2D groundCheckCollider;
  

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

    private void FixedUpdate()
    {
        GroundCheck();
        if (!isFinished)
        {
            HandleMovement();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasHitWall = true;
    }



    void GroundCheck()
    {
        List<Collider2D> results = new List<Collider2D>();
        if(groundCheckCollider.OverlapCollider(new ContactFilter2D(), results) <= 1)
        {
            isOnGround = false;
            rb.velocity = new Vector3(0, 0, 0);
            transform.position += (new Vector3(0, -1, 0)).normalized * 9.8f * Time.deltaTime;
        }
        else
        {
            isOnGround = true;
        }
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
        if(isOnGround)
        {
            float distCovered = (Time.deltaTime - moveStartTime) * moveSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / currentState.value;

            transform.position = Vector3.Lerp(transform.position, moveEndPosition, Math.Abs(fractionOfJourney));
        }

        if ((isOnGround && hasHitWall) || Math.Abs(transform.position.x - moveEndPosition.x) <= 0.25)
        {
            transform.position += (new Vector3(-1 * currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
            isMoving = false;
            SwitchState();
        }
    }

    void MoveToWall()
    {
        if(isOnGround && hasHitWall)
        {
            transform.position += (new Vector3(-1 * currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
            SwitchState();
            hasHitWall= false;
            return;
        }
        if (isOnGround)
        {
            transform.position += (new Vector3(currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
        }
    }
}
