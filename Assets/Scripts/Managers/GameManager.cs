using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState _currentGameState;
    private void Start()
    {
        _currentGameState = GameState.Play;
    }
    private void Update()
    {
        if(_currentGameState == GameState.Play) // Sadece Play state'i içerisindeysek kontrol yap.
        {
            WonCheck();
        }
    }
    private void WonCheck()
    {
        if(BabeCounter.Instance.GetCurrentScore() == 2)
        {
            ChangeState(GameState.GameOver);
        }
    }
    private void ChangeState(GameState newState)
    {
        _currentGameState = newState;
        switch (_currentGameState)
        {
            case GameState.GameOver:
                Debug.Log("YOU WON!");
                break;
        }
    }

    
}


