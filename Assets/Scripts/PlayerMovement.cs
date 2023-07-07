using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Tooltip("1 for Jump Right\n2 for Jump Left\n3 for Move By Amount Right\n4 for Move Right till Wall\n5 Move Amount to Left\n6 Move Left till Wall\n7 for Attack")]
    [SerializeField]
    List<int> actions;

    
    [SerializeField]
    int jumpHeight;



    bool isGrounded;
    Rigidbody2D rb;
    int currentState;
    Queue<int> actionsQueue;

    public void SwitchState()
    {
        if(actionsQueue.Any())
        {
            currentState = actionsQueue.Dequeue();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        actionsQueue = new Queue<int>(actions);
        currentState = actionsQueue.Dequeue();
    }

    private void FixedUpdate()
    {
        if(actionsQueue.Any())
        {
            StartCoroutine(HandleMovement());
        }
    }

    IEnumerator HandleMovement()
    {
        if (currentState == 1)
        {
            yield return JumpRight();
        }
        if (currentState == 2)
        {
            yield return JumpLeft();
        }
    }

    IEnumerator JumpRight()
    {
        if (rb.velocity.y == 0)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y));
            rb.AddForce(new Vector2(2, jumpForce), ForceMode2D.Impulse);
            yield return new WaitUntil(() => rb.velocity.y == 0);
        }
        SwitchState();
    }

    IEnumerator JumpLeft()
    {
        if (rb.velocity.y == 0)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y));
            rb.AddForce(new Vector2(-2, jumpForce), ForceMode2D.Impulse);
            yield return new WaitUntil(() => rb.velocity.y == 0);
        }
        SwitchState();
    }

    void MoveRight()
    {

    }
}
