using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// -----------------------------------------------
// Code taken from Unity's Open Project
// -----------------------------------------------

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IDialogueActions
{
    // Assign delegate{} to events to initialise them with an empty delegate
    // so we can skip the null check when we use them

    // Gameplay
    public event UnityAction interactEvent = delegate { }; // Used to talk, pickup objects, interact with tools like the cooking cauldron
    public event UnityAction openInventoryEvent = delegate { }; // Used to bring up the inventory
    public event UnityAction<Vector2> moveEvent = delegate { };

    public event UnityAction advanceDialogueEvent = delegate { };


    private GameInput gameInput;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Gameplay.SetCallbacks(this);
            gameInput.Dialogue.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        moveEvent.Invoke(_context.ReadValue<Vector2>());
    }

    public void OnInteract(InputAction.CallbackContext _context)
    {
        if (_context.phase == InputActionPhase.Performed)
            interactEvent.Invoke();
    }

    public void OnOpenInventory(InputAction.CallbackContext _context)
    {
        openInventoryEvent.Invoke();
    }

    public void OnAdvanceDialogue(InputAction.CallbackContext _context)
    {
        if (_context.phase == InputActionPhase.Performed)
            advanceDialogueEvent.Invoke();
    }

    public void EnableGameplayInput()
    {
        gameInput.Dialogue.Disable();

        gameInput.Gameplay.Enable();
    }

    public void EnableDialogueInput()
    {
        gameInput.Gameplay.Disable();

        gameInput.Dialogue.Enable();
    }

    public void DisableAllInput()
    {
        gameInput.Gameplay.Disable();
        gameInput.Dialogue.Disable();
    }
}
