using UnityEngine;
public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private float _rotationSpeedX = 100f; // Hızı kontrol etmek için.(Hassasiyet.)
    [SerializeField] private float _rotationSpeedY = 100f;

    private void Update()
    {
        mouseX = Input.GetAxis(Consts.MouseInputs.MOUSE_X);
        mouseY = Input.GetAxis(Consts.MouseInputs.MOUSE_Y);

        transform.eulerAngles += new Vector3(0, mouseX * _rotationSpeedX * Time.deltaTime, 0);
        transform.eulerAngles -= new Vector3(mouseY * _rotationSpeedY * Time.deltaTime, 0, 0);
    }
}
