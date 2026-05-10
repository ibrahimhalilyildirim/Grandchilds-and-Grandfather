using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreferencesPopupButtonUI : MonoBehaviour
{
    public static PreferencesPopupButtonUI Instance{get; private set;}

    [Header("Buttons")]
    [SerializeField] private Button _preferencesButton;
    [SerializeField] private Button _retryButton;
   
    [Header("References")]
    private GameManager _gameManager;
    [SerializeField] private GameObject _preferencesUI;
    private bool _isLosePopupActive = false;
    

    [Header("Game Objects")]
    [SerializeField] private GameObject _losePopupUI;
    // [SerializeField] private GameObject _preferencesSprite;
    // [SerializeField] private GameObject _retrySprite;
    [SerializeField] bool _isPreferencesButtonActive = false;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _retryButton.onClick.AddListener(OnRetryButtonClicked);
        _preferencesButton.onClick.AddListener(OnPreferencesButtonClicked);
    }

    private void OnPreferencesButtonClicked()
    {
        _preferencesUI.SetActive(!_isPreferencesButtonActive);
        _isPreferencesButtonActive = !_isPreferencesButtonActive;
    }

    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.LEVEL_ONE_SCENE);
    }
    public void GetOnPreferencesButtonClicked()
    {
        if(_losePopupUI != null)
        {
            if(_isLosePopupActive == false)
            {
                _isLosePopupActive = true;
                _losePopupUI.SetActive(true);
            } 
        }
    }
    public void CloseLosePopupUI()
    {
        _losePopupUI.SetActive(false);
    }
}
