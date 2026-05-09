using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    private PlayerController _playerController;
    private StateController _stateController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();
    }
    private void SetPlayerAnimatons()
    {
        var currentState = _stateController.GetCurrentState();
        switch (currentState)
        {
            case PlayerState.Idle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                break;
            case PlayerState.Move:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;
            case PlayerState.Jump:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
                break;
        }
    }
    private void Update()
    {
        SetPlayerAnimatons();
    }
}
