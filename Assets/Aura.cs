using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    [SerializeField] HauntableObject haunt;
    [SerializeField] Animator animator;

    public void StartAuraAnimation()
    {
        animator.SetBool("isHaunting", true);
    }

    public void CancelAnimation()
    {
        if (animator.GetBool("isHaunting"))
        {
            animator.SetBool("isHaunting", false);
        }
    }

    public void HauntObject()
    {
        haunt.StartHauntingAnimation();
        animator.SetBool("isHaunting", false);
    }
}
