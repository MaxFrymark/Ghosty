using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] bool canInteract = true;
    public bool CanInteract { get { return canInteract; } set { canInteract = value; } }
    HauntableObject interactable;

    public void AttemtInteract()
    {
        if (canInteract)
        {
            if (interactable != null && !interactable.IsHaunted)
            {
                interactable.BeginHauntingProcess();
            }
        }
    }

    public void AttemtCancel()
    {
        if (canInteract)
        {
            if (interactable != null && !interactable.IsHaunted)
            {
                interactable.StopHauntingProcess();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            interactable = collision.GetComponent<HauntableObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            interactable = null;
        }
    }
}
