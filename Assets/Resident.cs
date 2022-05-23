using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resident : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Qmark qmark;

    Vector2 moveDirection = Vector2.zero;
    float moveSpeed;
    float walkSpeed = 2f;
    float runSpeed = 10f;

    HauntableObject investigationTarget;

    enum State { Normal, Investigating, Nervous, Panicked }
    [SerializeField] State currentState = State.Normal;


    LayerMask hauntableObjectLayer;
    LayerMask doorLayer;

    ResidentTracker tracker;


    private void Start()
    {
        tracker = FindObjectOfType<ResidentTracker>();
        tracker.AddResident(this);
        moveSpeed = walkSpeed;
        moveDirection = Vector2.right;
        hauntableObjectLayer = LayerMask.GetMask(LayerMask.LayerToName(10));
        doorLayer = LayerMask.GetMask(LayerMask.LayerToName(13));
    }


    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        ChangeDirection();
        if(MovingNormally())
        {
            //Debug.Log("looking");
            Looking();
        }
        else if (currentState == State.Investigating)
        {
            CheckIfAtInvestigationTarget();
        }
    }

    private void Looking()
    {
        HauntableObject haunt = null;
        Vector2 dir = new Vector2(transform.localScale.x, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10f, hauntableObjectLayer | doorLayer);
        if (hit)
        {
            haunt = hit.collider.GetComponent<HauntableObject>();
        }
        if (haunt != null)
        {
            if (haunt.IsHaunted)
            {
                Investigate(haunt);
            }
        }
    }



    public void StartRunning()
    {
        moveSpeed = runSpeed;
        moveDirection = FindCenter();
    }

    public void ChangeDirection()
    {
        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void StartPanic()
    {
        currentState = State.Panicked;
        tracker.RemoveResident(this);
        PauseMovement();
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
        moveDirection = newDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Waypoint waypoint = collision.GetComponent<Waypoint>();
        if(waypoint != null)
        {
            if (MovingNormally() || !MovingNormally() && !waypoint.NormalPath)
            {
                moveDirection = waypoint.Direction;
            }
        }
    }

    private bool MovingNormally()
    {
        bool movingNormal = currentState == State.Normal | currentState == State.Nervous;
        //Debug.Log(movingNormal);
        return movingNormal;
        /*if(currentState == State.Normal)
        {
            return true;
        }

        else if(currentState == State.Nervous)
        {
            return true;
        }

        else
        {
            return false;
        }*/
    }

    public void PauseMovement()
    {
        moveSpeed = 0;
    }

    public void ResumeMovement()
    {
        if (currentState == State.Panicked)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void Investigate(HauntableObject haunt)
    {
        if (currentState == State.Nervous)
        {
            qmark.StopCountDown();
            StartPanic();
        }
        else if(currentState == State.Normal)
        {
            currentState = State.Investigating;
            qmark.gameObject.SetActive(true);
            investigationTarget = haunt;
            currentState = State.Investigating;
            moveDirection = new Vector2(transform.localScale.x, 0);
            StartCoroutine(WaitToResumeMovement());
        }
    }

    private void CheckIfAtInvestigationTarget()
    {
        if(Vector2.Distance(transform.position, new Vector2(investigationTarget.transform.position.x, transform.position.y)) < 0.1f)
        {
            PauseMovement();
            investigationTarget.ResetHaunting();
            investigationTarget = null;
            currentState = State.Nervous;
            StartCoroutine(WaitToResumeMovement());
            moveDirection = Vector2.right;
            qmark.StartCountdown();
        }
    }

    

    private IEnumerator WaitToResumeMovement()
    {
        PauseMovement();
        yield return new WaitForSeconds(1f);
        ResumeMovement();
    }

    public void ReturnToNormalState()
    {
        currentState = State.Normal;
    }
}
