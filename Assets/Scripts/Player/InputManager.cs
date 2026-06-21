using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FunnyWheel.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset playerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Sprint { get; private set; }
        public bool Jump { get; set; }

        private InputActionMap _inputActionMap;
        
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _sprintAction;
        private InputAction _jumpAction;

        private void Awake()
        {
            _inputActionMap = playerInput.FindActionMap("Player");
            _moveAction = _inputActionMap.FindAction("Move");
            _lookAction = _inputActionMap.FindAction("Look");
            _jumpAction = _inputActionMap.FindAction("Jump");
            _sprintAction = _inputActionMap.FindAction("Sprint");

            _moveAction.performed += OnMove;
            _lookAction.performed += OnLook;
            _jumpAction.performed += OnJump;
            _sprintAction.performed += OnSprint;
            
            _moveAction.canceled += OnMove;
            _lookAction.canceled += OnLook;
            _jumpAction.canceled += OnJump;
            _sprintAction.canceled += OnSprint;
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            Move = ctx.ReadValue<Vector2>();
        }

        private void OnLook(InputAction.CallbackContext ctx)
        {
            Look = ctx.ReadValue<Vector2>();
        }

        private void OnSprint(InputAction.CallbackContext ctx)
        {
            Sprint = ctx.ReadValueAsButton();
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            Jump = ctx.ReadValueAsButton();
        }

        private void OnEnable()
        {
            _inputActionMap.Enable();
        }

        private void OnDisable()
        {
            _inputActionMap.Disable();
        }
    }
}