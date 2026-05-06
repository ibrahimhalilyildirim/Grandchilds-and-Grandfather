using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform _orentationTransform;
    [SerializeField] private float _movementSpeed;
    private bool _isCurrentlyMoving = false;

    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    public float forceMultiplier;
    
    [Header("Jump Settings")]

    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;

    [Header("Visual Settings")]
    [SerializeField] private Transform _playerVisual; // Karakter modelinin olduğu obje.
    [SerializeField] private float _rotationSpeed = 10f; // Karakter modelinin dönüş hızı.
    private StateController _stateController;

    private void RotatePlayerViusal()
    {
        if(_movementDirection != Vector3.zero) // hareket yönü sıfır değilse.(Yani karakter bir yöne basıyorsa.)
        {
            // Gitmek istediğimiz yöne doğru bir hedef rotasyon oluşturuyoruz.
            Quaternion targetRotation = Quaternion.LookRotation(_movementDirection.normalized);

            // Karakterin mevcut rotasyonundan hedef rotasyona yumuşak bir geçiş yapıyoruz.
            _playerVisual.rotation = Quaternion.Slerp(_playerVisual.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }


    // Awake: Obje doğar doğmaz (henüz "gözlerini açmadan") çalışır. Sadece 1 kez çalışır.
    // OnEnable: Obje aktif olduğu anda çalışır. Obje her kapatılıp açıldığında (tikini kaldırıp geri açtığında) tekrar tekrar çalışır.
    // Start: Obje "hayata başlamadan" hemen önce çalışır. Sadece 1 kez çalışır.
    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void SetPlayerMovement()
    {
        _movementDirection = _orentationTransform.forward * _verticalInput
        + _orentationTransform.right * _horizontalInput;

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
    }
    private void SetPlayerJumping()
    {
        if (Input.GetKeyDown(_jumpKey))
        {
            _playerRigidbody.linearVelocity = new UnityEngine.Vector3(_playerRigidbody.linearVelocity.x,
            0f, _playerRigidbody.linearVelocity.z);

            _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void Update()
    {
        SetPlayerJumping();
        SetInputs();
        RotatePlayerViusal();
        SetStates();
    }
    private void SetStates()
    {
        var movementDirection = _movementDirection.normalized;
        var currentState = _stateController.GetCurrentState();
        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero => PlayerState.Idle,
            _ when movementDirection != Vector3.zero => PlayerState.Move,
            _ => currentState
        };
        if(newState != currentState)
        {
            _stateController.ChangeState(newState);
        }
        Debug.Log("New States: " + newState);
    }


}

