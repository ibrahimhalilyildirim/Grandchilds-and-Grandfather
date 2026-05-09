using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsControllerUI : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();
        if(_button != null)
        {
            _button.onClick.AddListener(LoadTargetLevel);
        }
    }
    public void LoadTargetLevel()
    {
        if (!string.IsNullOrEmpty(_sceneToLoad))
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
        
    }
}
