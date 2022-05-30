using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Cat
{
    [SerializeField] Animator animator;
    [SerializeField] Transform rayPoint;
    float walkSpeed = 1.5f;
    float runSpeed = 3.5f;

    bool runningTowardsObject = false;

    LayerMask hauntableObjectLayer;
    LayerMask doorLayer;

    HauntableObject haunt;


    protected override void Start()
    {
        base.Start();
        hauntableObjectLayer = LayerMask.GetMask(LayerMask.LayerToName(10));
        doorLayer = LayerMask.GetMask(LayerMask.LayerToName(13));
    }

    protected override void Update()
    {
        base.Update();
        SelectAnimation();
        if (!runningTowardsObject)
        {
            LookForHauntings();
        }
    }

    private void SelectAnimation()
    {
        if(speed > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void LookForHauntings()
    {
        haunt = null;
        Vector2 dir = new Vector2(transform.localScale.x, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, dir, 10f, hauntableObjectLayer | doorLayer);
        if (hit)
        {
            haunt = hit.collider.GetComponent<HauntableObject>();
        }
        if (haunt != null)
        {
            if (haunt.HauntingInProcess)
            {
                destination = new Vector2(haunt.transform.position.x, transform.position.y);
                speed = runSpeed;
                runningTowardsObject = true;
            }
        }
    }

    protected override void CheckIfAtDestination()
    {
        if (!runningTowardsObject)
        {
            base.CheckIfAtDestination();
        }

        else
        {
            float distance = Vector2.Distance(transform.position, destination);
            if(distance < 1f)
            {
                BarkAtObject();
            }
        }
    }

    private void BarkAtObject()
    {
        haunt.LockHaunting();
        speed = 0f;
        destination = SelectDestiantion();
        audioSource.Play();
        StartCoroutine(Barking());
    }

    private IEnumerator Barking()
    {
        animator.SetBool("isBarking", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isBarking", false);
        speed = walkSpeed;
        runningTowardsObject = false;
        audioSource.Stop();
    }
}
