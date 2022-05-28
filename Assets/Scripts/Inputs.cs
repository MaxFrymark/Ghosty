using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [SerializeField] PlayerMover mover;
    [SerializeField] PlayerInteraction interaction;
    [SerializeField] MenuScreen menuScreen;
    
    PlayerInputActions inputActions;

    [SerializeField] bool playerControl = true;
    public bool PlayerControl {get {return playerControl;} set { playerControl = value; } }
    
    void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += MovementPerformed;
        inputActions.Player.Move.canceled += MovementPerformed;
        inputActions.Player.Interact.performed += InterationPerformed;
        inputActions.Player.Interact.canceled += InterationCanceled;
        inputActions.Player.MainMenu.performed += OpenMainMenu;
    }


    public void MovementPerformed(InputAction.CallbackContext context)
    {
        if (playerControl)
        {
            if (mover != null)
            {
                mover.MovePlayer(context.ReadValue<Vector2>());
            }
        }
    }

    public void InterationPerformed(InputAction.CallbackContext context)
    {
        if (playerControl)
        {
            if (interaction != null)
            {
                interaction.AttemtInteract();
            }
        }
        else
        {
            menuScreen.SpacebarPressed();
        }
    }

    public void InterationCanceled(InputAction.CallbackContext context)
    {
        if (playerControl)
        {
            if (interaction != null)
            {
                interaction.AttemtCancel();
            }
        }
    }

    public void OpenMainMenu(InputAction.CallbackContext context)
    {
        if (playerControl)
        {
            if (menuScreen != null)
            {
                menuScreen.gameObject.SetActive(true);
                menuScreen.SelectMenu(MenuScreen.Menus.MainMenu);
            }
        }
        else
        {
            menuScreen.EscapePressed();
        }
    }
}
