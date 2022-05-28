using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qmark : MonoBehaviour
{
    [SerializeField] Resident resident;
    [SerializeField] Animator animator;

    public void ReturnToNormal()
    {
        resident.ReturnToNormalState();
        gameObject.SetActive(false);
    }

    public void StartCountdown()
    {
        animator.SetBool("isDecaying", true);
    }

    public void StopCountDown()
    {
        animator.SetBool("isDecaying", false);
        gameObject.SetActive(false);
    }
}
