using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] bool isHaunted = false;
    public bool IsHaunted { get { return isHaunted; } }
    [SerializeField] Animator animator;


    public void StartHauntingAnimation()
    {
        animator.SetTrigger("haunt");
    }

    public void HauntObject()
    {
        isHaunted = true;

    }
}
