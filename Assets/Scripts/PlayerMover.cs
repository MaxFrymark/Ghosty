using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] PlayerInteraction interaction;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    
    float m_Speed = 2f;
    Vector2 posChange;

    float maxX = 7.6f;
    [SerializeField] float maxY;

    bool beingPulled = false;
    public bool BeingPulled { get { return beingPulled; } }
    Vector2 pullDestination;

    
    public void MovePlayer(Vector2 playerMove)
    {
        posChange = playerMove;
    }

    private void Update()
    {
        MovePlayer();
        ChooseFacing();
        if (beingPulled)
        {
            transform.position = Vector2.MoveTowards(transform.position, pullDestination, 4f * Time.deltaTime);
        }
    }

    private void MovePlayer()
    {
        //transform.Translate(posChange * m_Speed * Time.deltaTime);
        transform.position = UpdatePos();
    }

    private void ChooseFacing()
    {
        if(posChange.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if(posChange.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private Vector2 UpdatePos()
    {
        Vector2 pos = ((Vector2)transform.position + posChange * m_Speed * Time.deltaTime) ;
        Vector2 newPos = new Vector2(Mathf.Clamp(pos.x, -maxX, maxX), Mathf.Clamp(pos.y, -6.5f, maxY));
        return newPos;
    }

    public void PullGhost(Vector2 destination)
    {
        audioSource.Play();
        animator.SetBool("beingHeld", true);
        m_Speed = 0;
        interaction.CanInteract = false;
        beingPulled = true;
        pullDestination = destination;
    }

    private void RestoreControl()
    {
        audioSource.Stop();
        animator.SetBool("beingHeld", false);
        beingPulled = false;
        m_Speed = 2f;
        interaction.CanInteract = true;
    }
}
