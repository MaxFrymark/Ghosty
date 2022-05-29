using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HauntableObject : MonoBehaviour
{
    [SerializeField] bool isHaunted = false;
    public bool IsHaunted { get { return isHaunted; } }
    [SerializeField] Animator animator;
    [SerializeField] Aura aura;
    public Aura Aura { get { return aura; } }
    PlayerInteraction player = null;

    LayerMask person;
    LayerMask door;

    bool hauntingInProcess = false;
    bool hauntingLocked = false;
    public bool HauntingInProcess { get { return hauntingInProcess; } set { hauntingInProcess = value; } }

    private void Update()
    {
        person = LayerMask.GetMask(LayerMask.LayerToName(8));
        door = LayerMask.GetMask(LayerMask.LayerToName(13));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hauntingLocked)
        {
            if (collision.GetComponent<PlayerInteraction>() != null)
            {
                player = collision.GetComponent<PlayerInteraction>();
                aura.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>() != null)
        {
            player = null;
            aura.gameObject.SetActive(false);
        }
    }

    public void StartHauntingAnimation()
    {
        hauntingInProcess = false;
        animator.SetBool("isHaunted", true);
        HauntObject();
    }

    private void HauntObject()
    {
        if (!isHaunted)
        {
            isHaunted = true;
            CheckForLOS(Vector2.right);
            CheckForLOS(Vector2.left);
        }
    }


    private void CheckForLOS(Vector2 dir)
    {
        Resident target = null;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10f, person | door);
        if (hit)
        {
            if (hit.collider.GetComponent<Resident>() != null)
            {
                target = hit.collider.GetComponent<Resident>();
            }
        }

        if(target != null)
        {
            if (CheckFacing(target))
            {
                target.StartPanic();
            }
        }
    }

    private bool CheckFacing(Resident target)
    {
        
        if (target.transform.localScale.x < 0)
        {
            if (transform.position.x <= target.transform.position.x)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (target.transform.localScale.x > 0)
        {
            if (transform.position.x >= target.transform.position.x)
            {
                return true;
            }
            else return false;
        }

        else return false;
    }

    public void BeginHauntingProcess()
    {
        aura.StartAuraAnimation();
        hauntingInProcess = true;
    }

    public void StopHauntingProcess()
    {
        aura.CancelAnimation();
        hauntingInProcess = false;
    }

    public void ResetHaunting()
    {
        animator.SetBool("isHaunted", false);
        isHaunted = false;
    }

    public void LockHaunting()
    {
        hauntingLocked = true;
        StopHauntingProcess();
        aura.gameObject.SetActive(false);
        StartCoroutine(LockCountdown());
    }

    private IEnumerator LockCountdown()
    {
        yield return new WaitForSeconds(3);
        hauntingLocked = false;
        if(player != null)
        {
            aura.gameObject.SetActive(true);
        }
    }
}
