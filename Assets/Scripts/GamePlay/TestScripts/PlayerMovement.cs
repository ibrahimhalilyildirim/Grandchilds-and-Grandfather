using System;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

public event Action<PlayerState> OnplayerStateChanged;

[Header("Movement Settings")]
[SerializeField] private float _movementSpeed;
[SerializeField] private Transform _orientation;
private float _horizontalInput, _verticalInput;
private Vector3 _movementDirection;
private Rigidbody _playerRigidbody;

private StateController _stateController;

[Header("Rotation Settings")]
[SerializeField] private float _rotationSpeed = 10f;
[SerializeField] private Transform _playerVisual;


[Header("Jump Settings")]
[SerializeField] private float _jumpForce;
[SerializeField] private float _jumpCooldown;
[SerializeField] private float _airMultiplier;
[SerializeField] private bool _canJump;
[SerializeField ] private KeyCode jumpKey;

[Header("Slope Handling")]
[SerializeField] private float _maxSlopeAngle;
[SerializeField] private bool existingSlope;
private RaycastHit _slopeHit;

[Header("Ground Check")]
[SerializeField] private float _playerHeight;
[SerializeField] private LayerMask _groundLayer;
[SerializeField] private float _groundDrag;
private bool _isGrounded;
    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _stateController = GetComponent<StateController>();
    }
    private void Start()
    {
        _canJump = true;
        _playerRigidbody.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void SetCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.3f, _groundLayer);

        SetInputs();
        SpeedControl();
        SetStates();
        SetRotation();
        SetCursor();

        if (_isGrounded)
        {
            _playerRigidbody.linearDamping = _groundDrag;
        }
        else
        {
            _playerRigidbody.linearDamping = 0;
        }

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(jumpKey) && _canJump && _isGrounded){
            _canJump = false;
            Jump();
            Invoke(nameof(ResetJump), _jumpCooldown);
        }
    }
    private void SetRotation()
    {
        Vector3 flatMovementDirection = new Vector3(_movementDirection.x, 0f, _movementDirection.z);
        if(flatMovementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatMovementDirection);
            _playerVisual.rotation = Quaternion.Slerp(_playerVisual.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }

    }
    private void MovePlayer()
    {
        _playerRigidbody.useGravity = !OnSlope(); // Karakter eğimli bir yüzeyde ise yerçekimi kapatılıyor ki karakter kaymasın.
        _movementDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        if (OnSlope() && !existingSlope)
        {
            _playerRigidbody.AddForce(GetSlopeMoveDirection() * _movementSpeed * 20f, ForceMode.Force);
            if(_playerRigidbody.linearVelocity.y > 0)
            {
                _playerRigidbody.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        else if (_isGrounded)
        {
            _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * 10f, ForceMode.Force);
        }
        else if(!_isGrounded)
        {
            _playerRigidbody.AddForce(_movementDirection.normalized * _airMultiplier * 10f, ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        if (OnSlope() && !existingSlope)
        {
            if(_playerRigidbody.linearVelocity.magnitude > _movementSpeed)
            {
                _playerRigidbody.linearVelocity = _playerRigidbody.linearVelocity.normalized * _movementSpeed;
            }
        }
        else
        {
            Vector3 flatVel = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

            if(flatVel.magnitude > _movementSpeed)
            {
            Vector3 limitedVel = flatVel.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity = new Vector3(limitedVel.x, _playerRigidbody.linearVelocity.y, limitedVel.z);
            }
        }
        
    }
    private void Jump()
    {
        existingSlope = true;
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        _canJump = true;
        existingSlope = false;
    }
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }
        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_movementDirection, _slopeHit.normal).normalized;
    }
    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var currentState = _stateController.GetCurrentState(); 

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded => PlayerState.Move,
            _ when !isGrounded => PlayerState.Jump,
            _ => currentState 
        };
        if(newState != currentState)
        {
            _stateController.GetChangeState(newState);
            OnplayerStateChanged?.Invoke(newState);
        }
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.3f, _groundLayer);
    }
    private Vector3 GetMovementDirection()
    {
        return _movementDirection.normalized;
    }
    
}
