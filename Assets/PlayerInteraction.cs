using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //[SerializeField]bool canInteract = false;
    HauntableObject interactable;

    public void AttemtInteract()
    {
        if (interactable != null && !interactable.IsHaunted)
        {
            interactable.Aura.StartAuraAnimation();
        }
    }

    public void AttemtCancel()
    {
        if(interactable != null && !interactable.IsHaunted)
        {
            interactable.Aura.CancelAnimation();
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
