using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerPoint;
    [SerializeField] private Transform followingPoint;
    private void FollowCamera()
    {
        transform.position = followingPoint.position;
        transform.LookAt(playerPoint);
    }
    private void LateUpdate()
    {
        FollowCamera();
    }
}
