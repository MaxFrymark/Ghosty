using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [SerializeField] PlayerMover mover;
    [SerializeField] PlayerInteraction interaction;
    
    PlayerInputActions inputActions;
    
    void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += MovementPerformed;
        inputActions.Player.Move.canceled += MovementPerformed;
        inputActions.Player.Interact.performed += InterationPerformed;
        inputActions.Player.Interact.canceled += InterationCanceled;
    }


    public void MovementPerformed(InputAction.CallbackContext context)
    {
        mover.MovePlayer(context.ReadValue<Vector2>());
    }

    public void InterationPerformed(InputAction.CallbackContext context)
    {
        interaction.AttemtInteract();
    }

    public void InterationCanceled(InputAction.CallbackContext context)
    {
        interaction.AttemtCancel();
    }
}
