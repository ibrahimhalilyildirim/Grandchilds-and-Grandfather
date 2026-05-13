using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public bool IsMusicMuted {get; private set;} = false;

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
    public void ToggleMusic()
    {
        IsMusicMuted = !IsMusicMuted;
        AudioListener.pause = IsMusicMuted;
    }

}
