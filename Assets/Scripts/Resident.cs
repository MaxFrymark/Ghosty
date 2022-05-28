using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resident : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] Qmark qmark;

    [SerializeField] protected Waypoint currentWayponint;
    [SerializeField] protected Waypoint nextWaypoint;
    [SerializeField] Waypoint escapeWaypoint;
    [SerializeField] protected Vector2 destination;

    protected float moveSpeed;
    [SerializeField] protected float walkSpeed = 2f;
    [SerializeField] protected float runSpeed = 10f;

    HauntableObject investigationTarget;

    enum State { Normal, Investigating, Nervous, Panicked }
    [SerializeField] State currentState = State.Normal;


    LayerMask hauntableObjectLayer;
    LayerMask doorLayer;

    ResidentTracker tracker;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] sounds;

    [SerializeField] bool hasCat;


    private void Start()
    {
        destination = currentWayponint.transform.position;
        tracker = FindObjectOfType<ResidentTracker>();
        tracker.AddResident(this);
        moveSpeed = walkSpeed;
        hauntableObjectLayer = LayerMask.GetMask(LayerMask.LayerToName(10));
        doorLayer = LayerMask.GetMask(LayerMask.LayerToName(13));
    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if(currentState != State.Investigating)
        {
            CheckIfAtDestination();
        }
        ChangeFacing();
        if(MovingNormally())
        {
            Looking();
        }
        else if (currentState == State.Investigating)
        {
            CheckIfAtInvestigationTarget();
        }
        WalkAnimation();
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

    private void WalkAnimation()
    {
        animator.SetBool("isWalking", moveSpeed != 0);
    }

    protected virtual void CheckIfAtDestination()
    {
        if((Vector2)transform.position == destination)
        {
            currentWayponint = nextWaypoint;
            destination = currentWayponint.transform.position;
        }
    }



    public void StartRunning()
    {
        moveSpeed = runSpeed;
        destination = escapeWaypoint.transform.position;
    }

    public void ChangeFacing()
    {
        if (destination.x >= transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else 
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void StartPanic()
    {
        currentState = State.Panicked;
        audioSource.volume = 1;
        audioSource.clip = sounds[1];
        audioSource.Play();
        tracker.RemoveResident(this);
        PauseMovement();
        animator.SetTrigger("panic");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Waypoint waypoint = collision.GetComponent<Waypoint>();
        if(waypoint != null && !waypoint.AgentOnly)
        {
            if (MovingNormally() || !MovingNormally() && !waypoint.NormalPath)
            {
                nextWaypoint = waypoint.NextWaypoint;
            }
        }
    }

    private bool MovingNormally()
    {
        return currentState == State.Normal | currentState == State.Nervous;
    }

    public void PauseMovement()
    {
        moveSpeed = 0;
    }

    public virtual void ResumeMovement()
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
            audioSource.clip = sounds[0];
            audioSource.Play();
            currentState = State.Investigating;
            qmark.gameObject.SetActive(true);
            investigationTarget = haunt;
            currentState = State.Investigating;
            destination = new Vector2(investigationTarget.transform.position.x, transform.position.y);
            StartCoroutine(WaitToResumeMovement());
        }
    }

    private void CheckIfAtInvestigationTarget()
    {
        if((Vector2)transform.position == destination)
        {
            PauseMovement();
            investigationTarget.ResetHaunting();
            investigationTarget = null;
            if (hasCat)
            {
                qmark.gameObject.SetActive(false);
                currentState = State.Normal;
            }

            else
            {
                currentState = State.Nervous;
                qmark.StartCountdown();
            }

            StartCoroutine(WaitToResumeMovement());
            destination = currentWayponint.transform.position;
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 0.5f;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        if (hasCat)
        {
            audioSource.clip = sounds[2];
            audioSource.Play();
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
