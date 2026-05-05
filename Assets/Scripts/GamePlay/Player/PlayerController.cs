using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform _orentationTransform;
    [SerializeField] private float _movementSpeed;

    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    public float forceMultiplier;
    
    [Header("Jump Settings")]

    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;

    // Awake: Obje doğar doğmaz (henüz "gözlerini açmadan") çalışır. Sadece 1 kez çalışır.
    // OnEnable: Obje aktif olduğu anda çalışır. Obje her kapatılıp açıldığında (tikini kaldırıp geri açtığında) tekrar tekrar çalışır.
    // Start: Obje "hayata başlamadan" hemen önce çalışır. Sadece 1 kez çalışır.
    private void Awake()
    {
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
        if (Input.GetKey(_jumpKey))
        {
            _playerRigidbody.linearVelocity = new UnityEngine.Vector3(_playerRigidbody.linearVelocity.x,
            0f, _playerRigidbody.linearVelocity.z);

            _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        SetInputs();
        SetPlayerMovement();
        SetPlayerJumping();
    }
}

