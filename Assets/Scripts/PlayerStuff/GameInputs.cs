using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }

    public event EventHandler OnPauseAction;

    private InputAction pauseAction;
    private PlayerInput playerInput;

    public enum Binding
    {
        Interact,
        Pause
    }

    private void Awake()
    {
        Instance = this;

        pauseAction = InputSystem.actions.FindAction("Pause");

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

    /*public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Pause:
               return pauseAction.ToDisplayString();

        }
    }*/
}
