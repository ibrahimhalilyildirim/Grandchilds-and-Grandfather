using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    private PlayerController _playerController;
    [SerializeField] private PlayerState _currentState = PlayerState.Idle; // Oluşturduğumuz enumu değişken olacak şekilde tanımlıyoruz.
    // C# için () fonksiyonu hemen çağırır. Bu yüzden event yapılarında kullanılmaz.
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        if(_playerController != null)
        {
           _playerController.OnMovementStateChanged += SetMovementState; 
        }
    } 
       private void OnDisable()
    {
        if(_playerController != null)
        {
           _playerController.OnMovementStateChanged -= SetMovementState; 
        }
    }
    private void SetMovementState(bool isMoving)
    {
        if (isMoving)
        {
            _currentState = PlayerState.Move;
            Debug.Log("Moving");
        }
        else
        {
            _currentState = PlayerState.Idle;
            Debug.Log("Idling");
        }
        SetPlayerAnimation();
    }
    private void SetPlayerAnimation()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                _playerAnimator.Play(Consts.PlayerState.PLAYER_IDLE);
                break;
            
            case PlayerState.Move:
                _playerAnimator.Play(Consts.PlayerState.PLAYER_WALKING);
                break;
        }
    }
}


