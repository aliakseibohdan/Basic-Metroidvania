using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Vector2 MoveInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public bool WasJumpPressed { get; private set; }
    public bool IsJumpBeingPressed { get; private set; }
    public bool WasJumpReleased { get; private set; }
    public bool IsRunBeingPressed { get; private set; }
    public bool WasDashPressed { get; private set; }
    public bool WasAttackPressed { get; private set; }
    public bool WasInteractPressed { get; private set; }
    public bool MenuOpenCloseInput { get; private set; }

    private static PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _mousePositionAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _dashAction;
    private InputAction _attackAction;
    private InputAction _interactAction;
    private InputAction _menuOpenCloseAction;

    // Control schemes.
    public static string GamepadControlScheme = "Gamepad";
    public static string KeyboardAndMouseControlScheme = "Keyboard + Mouse";

    public static string CurrentControlScheme {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void SetupInputActions()
    {
        _moveAction = _playerInput.actions["Move"];
        _mousePositionAction = _playerInput.actions["MousePosition"];
        _jumpAction = _playerInput.actions["Jump"];
        _runAction = _playerInput.actions["Run"];
        _dashAction = _playerInput.actions["Dash"];
        _attackAction = _playerInput.actions["Attack"];
        _interactAction = _playerInput.actions["Interact"];
        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
    }

    private void UpdateInputs()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
        MousePosition = _mousePositionAction.ReadValue<Vector2>();
        WasJumpPressed = _jumpAction.WasPressedThisFrame();
        IsJumpBeingPressed = _jumpAction.IsPressed();
        WasJumpReleased = _jumpAction.WasReleasedThisFrame();
        IsRunBeingPressed = _runAction.IsPressed();
        WasDashPressed = _dashAction.WasPressedThisFrame();
        WasAttackPressed = _attackAction.WasPressedThisFrame();
        WasInteractPressed = _attackAction.WasPressedThisFrame();
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }

    public static void DeactivatePlayerControls()
    {
        _playerInput.currentActionMap.Disable();
    }

    public static void ActivatePlayerControls()
    {
        _playerInput.currentActionMap.Enable();
    }

    public void SwitchControls(PlayerInput input)
    {
        CurrentControlScheme = input.currentControlScheme;
    }
}
