using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }

    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractAction;

    private InputAction pauseAction;
    private InputAction interactAction;
    private InputAction moveAction;

    public enum Binding
    {
        Interact,
        Pause,
        Up,
        Down,
        Left,
        Right
    }

    private void Awake()
    {
        InputSystem.actions.Enable();

        Instance = this;

        moveAction = InputSystem.actions.FindAction("Move");//

        pauseAction = InputSystem.actions.FindAction("Pause");

        interactAction = InputSystem.actions.FindAction("Interact2");

        pauseAction.performed += pauseAction_Performed;

        interactAction.performed += InteractAction_performed;

        //Debug.Log(GetBindingText(Binding.Pause));

        Debug.Log("GameInputs Started");
    }

    private void InteractAction_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovmentVectorNormilzed()
    {
        return moveAction.ReadValue<Vector2>().normalized;
    }

    private void pauseAction_Performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        pauseAction.performed -= pauseAction_Performed;
        interactAction.performed -= InteractAction_performed;

        pauseAction.Dispose();
        interactAction.Dispose(); 
        moveAction.Dispose();

        Debug.Log("GameInputs Destroyed");
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
            case Binding.Up:
                return moveAction.GetBindingDisplayString(2);
            case Binding.Down:
                return moveAction.GetBindingDisplayString(4);
            case Binding.Left:
                return moveAction.GetBindingDisplayString(6);
            case Binding.Right:
                return moveAction.GetBindingDisplayString(8);

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
            case Binding.Up:
                inputAction = moveAction;
                bindingIndex = 2;
                break;
            case Binding.Down:
                inputAction = moveAction;
                bindingIndex = 4;
                break;
            case Binding.Left:
                inputAction = moveAction;
                bindingIndex = 6;
                break;
            case Binding.Right:
                inputAction = moveAction;
                bindingIndex = 8;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                //Debug.Log(callback.action.bindings[0].path);
                //Debug.Log(callback.action.bindings[0].overridePath);
                callback.Dispose();
                InputSystem.actions.Enable();
                onActionRebound();

            }).Start();
    }
}
