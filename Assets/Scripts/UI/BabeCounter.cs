using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BabeCounter : MonoBehaviour
{
    public static BabeCounter Instance {get; private set; }
    [SerializeField] private TextMeshProUGUI _babeCounterText;
    [SerializeField] private int _currentScore = 0;
    [SerializeField] private string _targetTag = "CollectibleBabe";

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            IncrementBabeCount();
        }
    }
    private void IncrementBabeCount()
    {
        _currentScore++;
        UpdateCounterDisplay();
    }
    private void UpdateCounterDisplay()
    {
        if(_babeCounterText != null)
        { 
            _babeCounterText.text = "Current Score: " + _currentScore.ToString();   
        }
    }
    public int GetCurrentScore()
    {
        return _currentScore;
    }
}
