using TMPro;
using UnityEngine;

public class BabeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _babeCounterText;
    [SerializeField] private int _currentScore = 0;
    [SerializeField] private string _targetTag = "CollectibleBabe";
    private void Start()
    {
        UpdateCounterDisplay();
    }
    public void IncrementBabeCount()
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_targetTag))
        {
            IncrementBabeCount();
        }
    }
}
