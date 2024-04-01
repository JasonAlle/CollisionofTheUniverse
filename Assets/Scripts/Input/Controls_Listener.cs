using System;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(menuName ="ControlsListener")]
public class Controls_Listener : ScriptableObject, Game_Controls.IGameplayControlsActions, Game_Controls.IUIControlsActions
{
    private Game_Controls gameInput;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new Game_Controls();

            gameInput.GameplayControls.SetCallbacks(this);
            gameInput.UIControls.SetCallbacks(this);
            
            SetGameplay();
        }
        gameInput.GameplayControls.Enable();
    }
    private void OnDisable()
    {
        gameInput.GameplayControls.Enable();
    }

    public event Action<float> PlayerMoveEvent;
    public event Action<Vector2> UIMoveEvent;
    public event Action DropEvent;
    public event Action PauseEvent;
    public event Action ResumeEvent;
    public event Action BackEvent;
    public event Action ConfirmEvent;


    public void SetGameplay()
    {
      gameInput.GameplayControls.Enable();
        gameInput.UIControls.Disable();
    }
    public void SetUI()
    {
        gameInput.UIControls.Enable();
        gameInput.GameplayControls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        PlayerMoveEvent?.Invoke(context.ReadValue<float>());
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            DropEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SetUI();
            PauseEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        UIMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ConfirmEvent?.Invoke();
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            BackEvent?.Invoke();
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SetGameplay();
            ResumeEvent?.Invoke();
        }
    }
}
