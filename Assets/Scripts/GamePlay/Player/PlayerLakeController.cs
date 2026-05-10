using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLakeController : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lake"))
        {
            _gameManager._currentGameState = GameState.GameLose;
        }
    }
}
