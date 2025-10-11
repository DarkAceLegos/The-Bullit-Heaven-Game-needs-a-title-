using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }

    public event EventHandler OnPauseAction;

    private InputAction pauseAction;
    private InputAction interactAction;

    public enum Binding
    {
        Interact,
        Pause
    }

    private void Awake()
    {
        Instance = this;

        pauseAction = InputSystem.actions.FindAction("Pause");

        interactAction = InputSystem.actions.FindAction("Interact2");

        pauseAction.performed += pauseAction_Performed;

        //Debug.Log(GetBindingText(Binding.Pause));
    }

    private void pauseAction_Performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        pauseAction.performed -= pauseAction_Performed;

        pauseAction.Dispose();
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Pause:
               return pauseAction.GetBindingDisplayString();
            case Binding.Interact:
                return interactAction.GetBindingDisplayString();

        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound) 
    {
        InputSystem.actions.Disable();

        int bindingIndex;
        InputAction inputAction;

        switch (binding)
        {
            default:
            case Binding.Pause:
                inputAction = pauseAction;
                bindingIndex = 0;
                break;
            case Binding.Interact:
                inputAction = interactAction;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
        {
            Debug.Log(callback.action.bindings[0].path);
            Debug.Log(callback.action.bindings[0].overridePath);
            callback.Dispose();
            InputSystem.actions.Enable();
            onActionRebound();

        }).Start();
    }
}
