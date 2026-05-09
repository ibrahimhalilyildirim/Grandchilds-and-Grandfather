using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlayerState = PlayerState.Idle;
    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }
    public void ChangeState(PlayerState newPlayerState)
    {
        if(_currentPlayerState == newPlayerState) {return;}
            _currentPlayerState = newPlayerState;
        Debug.Log("Current State" + _currentPlayerState);
    }
    public PlayerState GetCurrentState()
    {
        return _currentPlayerState;
    }
    public void GetChangeState(PlayerState newState)
    {
        ChangeState(newState);
    }
}


