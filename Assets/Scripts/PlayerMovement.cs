using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    List<ActionObject> actions;

    [SerializeField]
    GameObject actionBar;

    ActionObject currentState;
    Queue<ActionObject> actionsQueue;

    [Header("Player Settings")]
    [SerializeField]
    int jumpHeight;
    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb;
    Vector3 moveEndPosition;

    bool isJumping = false, isMoving = false, isMovingToWall = false, isFinished = false, hasHitWall = false, isOnGround = true; bool isActing = false;

    float moveStartTime;

    [SerializeField]
    Collider2D groundCheckCollider;
    
    Animator animator;

    [SerializeField]
    Slider actionCompleteSlider;
    float sliderValue;

    bool isFacingLeft;

    // Start is called before the first frame update
    void Start()
    {
        PopulateActionBar();
        sliderValue = 1f/actions.Count;
        animator = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        actionsQueue = new Queue<ActionObject>(actions);
        

    }

    private void Update()
    {
        
        if (!isFinished)
        {
            if (isMoving) 
            {
                Move();
            }
            if(isMovingToWall)
            {
                MoveToWall();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !isActing)
        {
            isActing = true;
            StartActing();
        }
        GroundCheck();

    }

    public void StartActing()
    {
        StartCoroutine(HandleMovement());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall")) 
        {
            if(currentState.action == ActionObject.Action.MoveToWall || currentState.action == ActionObject.Action.Move)
            {
                hasHitWall = true;
            }
        }
    }

    void GroundCheck()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(7));
        if (!groundCheckCollider.IsTouching(contactFilter))
        {
            isOnGround = false;
            if (!isJumping)
            {
                rb.velocity = new Vector2(0, 0);
                transform.position += (new Vector3(0, -1, 0)).normalized * 9.8f * Time.deltaTime;
            } 
        }
        else
        {
            isOnGround= true;
        }
    }

    void FillActionComplete()
    {
        actionCompleteSlider.value += sliderValue;
    }

    IEnumerator HandleMovement()
    {
        foreach(ActionObject action in actions)
        {
            currentState = action;
            if(action.value< 0 && !isFacingLeft)
            {
                transform.Rotate(0f, 180f, 0f);
                isFacingLeft = true;
            }
            if (action.value > 0 && isFacingLeft)
            {
                transform.Rotate(0f, 180f, 0f);
                isFacingLeft = false;
            }
            if (action.action == ActionObject.Action.Jump)
            {
                isJumping= true;
                animator.SetBool("IsJumping", true);
                StartCoroutine(Jump());
                yield return new WaitUntil(() => !isJumping);
            }
            if(action.action == ActionObject.Action.Move)
            {
                moveEndPosition = new Vector3(transform.position.x + currentState.value, transform.position.y, transform.position.z);
                isMoving = true;
                yield return new WaitUntil(() => !isMoving);
            }
            if (action.action == ActionObject.Action.MoveToWall)
            {
                isMovingToWall = true;
                animator.SetBool("IsRunning", true);
                yield return new WaitUntil(() => !isMovingToWall);
            }
            FillActionComplete();
        }
    }

    IEnumerator Jump()
    {
        yield return new WaitUntil(() => isOnGround);
        rb.AddForce(new Vector2(currentState.value, 0), ForceMode2D.Impulse);
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y));
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => isOnGround);
        animator.SetBool("IsJumping", false);
        isJumping = false;     
    }

    void Move()
    {
        if(isOnGround)
        {
            float distCovered = (Time.deltaTime - moveStartTime) * moveSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / currentState.value;

            transform.position += (new Vector3(currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
        }

        if ((isOnGround && hasHitWall) || Math.Abs(transform.position.x - moveEndPosition.x) <= 0.25)
        {
            transform.position += (new Vector3(-1 * currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
            isMoving = false;
        }
    }

    void MoveToWall()
    {
        if(isOnGround && hasHitWall)
        {
            transform.position += (new Vector3(-1 * currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
            hasHitWall = false;
            animator.SetBool("IsRunning", false);
            isMovingToWall= false;
        }
        else
        {
            transform.position += (new Vector3(currentState.value, 0, 0)).normalized * moveSpeed * Time.deltaTime;
        }
    }


    void PopulateActionBar()

    {

        for (int i = 0; i < actions.Count; i++)

        {

            var img = Instantiate(actions[i].actionImage, actionBar.transform.position, Quaternion.identity);

            img.transform.SetParent(actionBar.transform);

        }

    }

    public void ResetQueue()
    {
        isActing = false;
        if (isMovingToWall)
        {
            hasHitWall = true;
        }
        StopAllCoroutines();
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsRunning", false);
        actionCompleteSlider.value = 0;
        if(isFacingLeft)
        {
            transform.Rotate(0f, 180f, 0f);
            isFacingLeft= false;
        }
    }
}
