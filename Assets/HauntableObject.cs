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

    LayerMask person;
    LayerMask door;

    private void Update()
    {
        person = LayerMask.GetMask(LayerMask.LayerToName(8));
        door = LayerMask.GetMask(LayerMask.LayerToName(13));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerInteraction>() != null)
        {
            aura.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerInteraction>() != null)
        {
            aura.gameObject.SetActive(false);
        }
    }

    public void StartHauntingAnimation()
    {
        animator.SetBool("isHaunted", true);
    }

    public void HauntObject()
    {
        isHaunted = true;
        CheckForLOS(Vector2.right);
        CheckForLOS(Vector2.left);
    }


    private void CheckForLOS(Vector2 dir)
    {
        Resident target = null;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10f, person | door);
        if (hit)
        {
            target = hit.collider.GetComponent<Resident>();
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
        //Debug.Log("meow");
        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance == 0)
        {
            return true;
        }
        else if (distance > 0 && target.transform.localScale.x > 0)
        {
            return true;
        }
        else if (distance < 0 && target.transform.localScale.x < 0)
        {
            return true;
        }

        else return false;
    }

    public void ResetHaunting()
    {
        animator.SetBool("isHaunted", false);
        isHaunted = false;
    }
}
