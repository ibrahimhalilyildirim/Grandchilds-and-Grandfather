using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLakeCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lake"))
        {
            SceneManager.LoadScene(Consts.SceneNames.LEVEL_ONE_SCENE);
        }
    }
}
