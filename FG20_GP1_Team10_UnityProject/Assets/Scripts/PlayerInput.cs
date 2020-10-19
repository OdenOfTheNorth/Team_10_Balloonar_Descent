using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private static PlayerControls input = null;

    private WeightPickup weightPickup = null;
    
    private void Awake()
    {
        if (input == null)
        {
            input = new PlayerControls();
            input.Enable();
        }

        weightPickup = GetComponentInChildren<WeightPickup>();
    }

    private void OnEnable()
    {
        BindInput();
    }

    private void OnDisable()
    {
        UnbindInput();
    }

    private void BindInput()
    {
        input.PlayerOne.WindControl.performed += ControlWindDirection;
        input.PlayerOne.WindControl.canceled += ControlWindDirection;

        // input.PlayerOne.TogglePickup.performed += SendWeightToggleInput;
        
        input.PlayerOne.QuitApplication.performed += QuitApplication;
    }

    private void UnbindInput()
    {
        input.PlayerOne.WindControl.performed -= ControlWindDirection;
        input.PlayerOne.WindControl.canceled -= ControlWindDirection;
        
        // input.PlayerOne.TogglePickup.performed -= SendWeightToggleInput;
        
        input.PlayerOne.QuitApplication.performed -= QuitApplication;
    }

    private void ControlWindDirection(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        WindManager.instance.currentWindDirection = direction;
    }

    private void SendWeightToggleInput(InputAction.CallbackContext context)
    {
        weightPickup.TogglePickup();
    }

    private void QuitApplication(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
