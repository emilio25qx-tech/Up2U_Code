using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float maxX = 45f;

    private float rotationX = 0;

    public void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxX, maxX);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(0, mouseX, 0);
    }
}
