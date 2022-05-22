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
        animator.SetTrigger("startDecay");
    }
}
