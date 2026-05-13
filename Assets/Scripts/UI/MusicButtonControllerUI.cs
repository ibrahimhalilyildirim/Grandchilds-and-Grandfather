using UnityEngine;
using UnityEngine.UI;

public class MusicButtonControllerUI : MonoBehaviour
{
    // MusicManager anlık olarak müziğin açık mı yoksa kapalı mı olduğunu tutar. Music button controller ise
    // MusicManager'a istediği zaman müziği açmasını veya kapatmasını söyler.
    // Eğer ki birisi MusicManager'a anlık olarak müzik açık mı yoksa kapalı mı diye sorarsa da son tuttuğu değeri iletir.

    [SerializeField] private Image _musicActiveIcon;
    [SerializeField] private Image _musicPassiveIcon;

    [SerializeField] private Button _musicButton;
    private void Awake()
    {
        _musicButton = GetComponent<Button>();
    }
    private void Start()
    {
        UpdateButtonVisuals(); // Her oluşturulduğunda MusicManager'a müzik ne durumda diye soruyor.
    }
    private void OnEnable()
    {
        _musicButton.onClick.AddListener(OnButtonPress);
    }
    private void OnDisable()
    {
        _musicButton.onClick.RemoveListener(OnButtonPress);
    }
    private void OnButtonPress()
    {
        if(MusicManager.Instance != null)
        {
            MusicManager.Instance.ToggleMusic();
            UpdateButtonVisuals();
        }
    }
    private void UpdateButtonVisuals()
    {
        if(MusicManager.Instance != null)
        {
            bool isMuted = MusicManager.Instance.IsMusicMuted;

             if (isMuted)
             {
                _musicActiveIcon.enabled = false;
                _musicPassiveIcon.enabled = true;
             }
                else
             {
                _musicActiveIcon.enabled = true;
                _musicPassiveIcon.enabled = false;
             }
        }
    }
}
