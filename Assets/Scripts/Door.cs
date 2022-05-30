using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audio;
    Resident personPassingThroughDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!animator.GetBool("isOpen")) 
        { 
            animator.SetBool("isOpen", true);
            audio.Play();
            personPassingThroughDoor = collision.GetComponent<Resident>();
            personPassingThroughDoor.PauseMovement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("isOpen", false);
    }

    public void AllowPersonToResmueMovement()
    {
        personPassingThroughDoor.ResumeMovement();
    }
}
