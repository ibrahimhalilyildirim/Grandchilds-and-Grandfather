using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButtonUI : MonoBehaviour
{
    [SerializeField] private Button _retryButton;
    private GameState _currentGameState;
    private void OnEnable()
    { // => lambda olarak geçiyormuş.
        _retryButton.onClick.AddListener(() => PreferencesPopupButtonUI.Instance.OnRetryButtonClicked()); 
        _currentGameState = GameState.Play;
    }
}
