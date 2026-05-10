using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState _currentGameState;
    [SerializeField] private TMPro.TextMeshProUGUI _babeCounterText;
    private void Start()
    {
        _currentGameState = GameState.Play;
    }
    private void Update()
    {
        if(_currentGameState == GameState.Play) // Sadece Play state'i içerisindeysek kontrol yap.
        {
            CloseLosePopup();
            WonCheck();
        }
        else if(_currentGameState == GameState.GameLose)
        {
            PreferencesPopupButtonUI.Instance.GetOnPreferencesButtonClicked();
            _currentGameState = GameState.Pause;
        }
    }
    private void WonCheck()
    {
        if(BabeCounter.Instance.GetCurrentScore() == 6)
        {
            ChangeState(GameState.GameWin);
        }
    }
    private void ChangeState(GameState newState)
    {
        _currentGameState = newState;
        switch (_currentGameState)
        {
            case GameState.GameWin:
                Debug.Log("YOU WON!");
                break;
        }
    }
    private void CloseLosePopup()
    {
        PreferencesPopupButtonUI.Instance.CloseLosePopupUI();
    }

    
}


