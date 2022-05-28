using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    [SerializeField] HauntableObject haunt;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audio;

    public void StartAuraAnimation()
    {
        animator.SetBool("isHaunting", true);
        audio.Play();
    }

    public void CancelAnimation()
    {
        if (animator.GetBool("isHaunting"))
        {
            animator.SetBool("isHaunting", false);
            audio.Stop();
        }
    }

    public void HauntObject()
    {
        haunt.StartHauntingAnimation();
        animator.SetBool("isHaunting", false);
    }
}
