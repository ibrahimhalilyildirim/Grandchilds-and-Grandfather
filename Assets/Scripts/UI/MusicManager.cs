using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {get; private set;}
    [SerializeField] private Image _musicActiveIcon;
    [SerializeField] private Image _musicPassiveIcon;
    [SerializeField] private bool isMusicMuted = false;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void OnButtonPress()
    {
        if (!isMusicMuted)
        {
            isMusicMuted = true;
            AudioListener.pause = true;
        }
        else if (isMusicMuted)
        {
            isMusicMuted = false;
            AudioListener.pause = false;
        }
        UpdateButtonIcon();
    }
    private void UpdateButtonIcon()
    {
        if (!isMusicMuted)
        {
            _musicActiveIcon.enabled = true;
            _musicPassiveIcon.enabled = false;
        }
        else
        {  
            _musicActiveIcon.enabled = false;
            _musicPassiveIcon.enabled = true;
        }

    }
}
