using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]bool canInteract = false;

    public void AttemtInteract()
    {
        if (canInteract)
        {
            FindObjectOfType<Resident>().StartPanic();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hi");

        if (collision.gameObject.tag == "Object")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            canInteract = false;
        }
    }
}
