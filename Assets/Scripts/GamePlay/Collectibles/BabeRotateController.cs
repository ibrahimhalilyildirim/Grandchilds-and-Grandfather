using Unity.VisualScripting;
using UnityEngine;

public class BabeRotateController : MonoBehaviour
{
    private Transform _playerTransform;
    private void Start()
    {
        _playerTransform = GameObject.Find("Player").transform;
    }
    void Update()
    {
        LookAtGrandfather();
    }
    private void LookAtGrandfather()
    {
        if(_playerTransform != null)
        {
            // Önce bebek yan yatmasın diye yüksekliği sabit tutuyoruz ardından bu değeri sürekli dedeye bakacak şekilde ayarlıyoruz.
            Vector3 targetPosition = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
            transform.LookAt(targetPosition);
        }
    }
}
