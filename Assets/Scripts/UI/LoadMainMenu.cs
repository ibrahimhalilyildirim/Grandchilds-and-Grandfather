using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GoMainMenu : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(Consts.SceneNames.MENU_SCENE));
    }
}
