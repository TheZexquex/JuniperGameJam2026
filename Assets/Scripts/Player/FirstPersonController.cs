using System;
using FunnyWheel.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private Rigidbody _rigidBody;
    [SerializeField] private InputManager inputManager;

    private Animator _animator;
    private bool _hasAnimator;
    private int _xVelHash;
    private int _yVelHash;
    private float _animationVelocityX;
    private float _animationVelocityY;

    public bool canMove { get; set; } = true;

    [Header("Movement Speed")] [SerializeField]
    private float walkSpeed = 10f;

    [SerializeField] private float sprintSpeed = 14f;
    [SerializeField] private float animationSmoothRate = 10f;

    [Header("Jump Parameters")] [SerializeField]
    private float jumpForce = 10f;

    [SerializeField] private float gravity = 30f;
    [SerializeField] private float groundedOffset = -0.14f;
    [SerializeField] private float groundedRadius = 0.28f;
    [SerializeField] private LayerMask groundLayers;

    [Header("Look Sensitivity")] [SerializeField, Range(0, 2)]
    private float xMouseSensitivity = 0.2f;

    [SerializeField, Range(0, 2)] private float yMouseSensitivity = 0.2f;
    [SerializeField, Range(1, 90)] private float maxUpperAngle = 85f;
    [SerializeField, Range(1, 90)] private float maxLowerAngle = 85f;

    private Vector2 _moveInput;
    private Vector2 _lookInput;

    private Camera _camera;
    private CharacterController _characterController;

    private Vector3 _moveDirection;
    private float _verticalRotation = 0f;
    private Vector3 _velocity;
    public bool _isGrounded { get; private set; } = true;
    private float _terminalVelocity = 53.0f;
    private float _verticalVelocity = 0f;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    [Header("Jump/Fall Timing")] [SerializeField]
    private float jumpTimeout = 0.1f;

    [SerializeField] private float fallTimeout = 0.15f;

    public bool IsMoving { get; private set; } = false;

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        HandleGravityAndJumping();
        GroundedCheck();
        HandleRotation();
        HandleMovement();
    }

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        _hasAnimator = TryGetComponent(out _animator);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _jumpTimeoutDelta = jumpTimeout;
        _fallTimeoutDelta = fallTimeout;
        _xVelHash = Animator.StringToHash("X_Velocity");
        _yVelHash = Animator.StringToHash("Y_Velocity");
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset,
            transform.position.z);
        _isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    private void HandleMovement()
    {
        float targetSpeed = inputManager.Sprint ? sprintSpeed : walkSpeed;

        if (inputManager.Move == Vector2.zero) targetSpeed = 0.0f;

        Vector3 inputDirection = new Vector3(inputManager.Move.x, 0.0f, inputManager.Move.y).normalized;

        if (inputManager.Move != Vector2.zero)
        {
            inputDirection = transform.right * inputManager.Move.x + transform.forward * inputManager.Move.y;
        }

        _characterController.Move(inputDirection.normalized * (targetSpeed * Time.deltaTime) +
                                  new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        if (_hasAnimator)
        {
            _animationVelocityX = Mathf.Lerp(_animationVelocityX, inputManager.Move.x * targetSpeed,
                Time.deltaTime * animationSmoothRate);
            _animationVelocityY = Mathf.Lerp(_animationVelocityY, inputManager.Move.y * targetSpeed,
                Time.deltaTime * animationSmoothRate);

            _animator.SetFloat(_xVelHash, _animationVelocityX);
            _animator.SetFloat(_yVelHash, _animationVelocityY);
        }
    }
    
    public float GetAnimationVelocityX() => _animationVelocityX;
    public float GetAnimationVelocityY() => _animationVelocityY;


    private void HandleGravityAndJumping()
    {
        if (_isGrounded)
        {
            _fallTimeoutDelta = fallTimeout;

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (inputManager.Jump && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(jumpForce * 2f * gravity);
                _jumpTimeoutDelta = jumpTimeout;
                _animator.SetBool(IsJumping, true);
            }

            if (_jumpTimeoutDelta > 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            _animator.SetBool(IsJumping, false);
            _jumpTimeoutDelta = jumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }

            inputManager.Jump = false;
        }

        if (_verticalVelocity > -_terminalVelocity)
        {
            _verticalVelocity -= gravity * Time.deltaTime;
        }
    }

    private void HandleRotation()
    {
        var mouseXRotation = inputManager.Look.x * xMouseSensitivity;
        transform.Rotate(Vector3.up * mouseXRotation);

        _verticalRotation -= inputManager.Look.y * yMouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -maxLowerAngle, maxUpperAngle);
        _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }


    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (_isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z),
            groundedRadius);
    }
}