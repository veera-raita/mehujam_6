using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class InputReader : ScriptableObject, InputSystem_Actions.IGameActions
{
    //inputsystem action map
    private InputSystem_Actions inputMap;

    //events to subscribe methods to in playercontroller et cetera
    public event Action<Vector2> PositionEvent;
    public event Action ClickEvent;
    public event Action ClickCanceledEvent;

    //state bools for checking if inputs are on
    public bool gameplayOn { get; private set; } = false;

    private void OnEnable()
    {
        if (inputMap == null)
        {
            inputMap = new();
            inputMap.Game.SetCallbacks(this);
        }
    }

    public void EnableGameplay()
    {
        gameplayOn = true;
        inputMap.Game.Enable();
    }

    public void DisableGameplay()
    {
        gameplayOn = false;
        inputMap.Game.Disable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ClickEvent?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            ClickCanceledEvent?.Invoke();
        }
    }

    public void OnPosition(InputAction.CallbackContext context)
    {
        PositionEvent?.Invoke(obj:context.ReadValue<Vector2>());
    }


}
