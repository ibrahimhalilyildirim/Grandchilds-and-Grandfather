using UnityEngine;

public class OrientationRotation : MonoBehaviour
{
    [SerializeField] private Transform _viewTransform;

    private void Update()
    {
        Vector3 viewDirection = transform.position - new Vector3(_viewTransform.position.x, transform.position.y, _viewTransform.position.z);
        // Eğer bir hareket varsa orientation'ı o tarafa çevir.
        if(viewDirection != Vector3.zero)
        {
            transform.forward = viewDirection.normalized;
        }
    }
}
