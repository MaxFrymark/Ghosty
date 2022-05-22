using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resident : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    Vector2 moveDiretion = Vector2.zero;
    float moveSpeed;
    float walkSpeed = 3f;
    float runSpeed = 10f;

    bool isPanicking = false;

    private void Start()
    {
        moveSpeed = walkSpeed;
        moveDiretion = Vector2.right;
    }


    private void Update()
    {
        if (moveDiretion != Vector2.zero)
        {
            transform.Translate(moveDiretion * moveSpeed * Time.deltaTime);
            ChangeDirection();
        }
    }

    public void StartRunning()
    {
        moveSpeed = runSpeed;
        moveDiretion = FindCenter();
    }

    public void ChangeDirection()
    {
        if (moveDiretion.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveDiretion.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        //transform.localScale = new Vector2(transform.localScale.x * -1, 1);
        //StartRunning();
    }

    public void StartPanic()
    {
        isPanicking = true;
        moveSpeed = 0;
        animator.SetTrigger("panic");
    }

    private Vector2 FindCenter()
    {
        if(transform.position.x < 0)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;
        }
    }

    public void InputNewDirection(Vector2 newDirection)
    {
        moveDiretion = newDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Waypoint waypoint = collision.GetComponent<Waypoint>();
        if(waypoint != null)
        {
            if (!isPanicking || isPanicking && !waypoint.IgnoreIfPanicked)
            {
                moveDiretion = waypoint.Direction;
            }
        }
    }
}
