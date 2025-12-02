using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCrouch : MonoBehaviour
{
    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float crouchSpeed = 1.5f;
    public float crouchTransitionSpeed = 5f;
    public Transform playerCamera;
    public float cameraCrouchOffset = 0.5f;

    private float originalHeight;
    private float originalCameraY;
    private float currentSpeed;

    private CharacterController controller;
    private PlayerMovement movementScript;
    private bool isCrouching = false;
    private bool isTransitioning = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        movementScript = GetComponent<PlayerMovement>();

        originalHeight = controller.height;
        currentSpeed = movementScript != null ? movementScript.walkSpeed : 2f;

        if (playerCamera != null)
            originalCameraY = playerCamera.localPosition.y;
    }

    public void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isTransitioning)
        {
            isCrouching = true;
            isTransitioning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && !isTransitioning)
        {
            isCrouching = false;
            isTransitioning = true;
        }

        if (isTransitioning)
        {
            float targetHeight = isCrouching ? crouchHeight : originalHeight;
            controller.height = Mathf.MoveTowards(controller.height, targetHeight, crouchTransitionSpeed * Time.deltaTime);

            float standSpeed = movementScript != null ? movementScript.walkSpeed : 2f;
            currentSpeed = Mathf.Lerp(currentSpeed, isCrouching ? crouchSpeed : standSpeed, Time.deltaTime * crouchTransitionSpeed);

            if (playerCamera != null)
            {
                float targetCamY = isCrouching ? originalCameraY - cameraCrouchOffset : originalCameraY;
                Vector3 camPos = playerCamera.localPosition;
                camPos.y = Mathf.MoveTowards(camPos.y, targetCamY, crouchTransitionSpeed * Time.deltaTime);
                playerCamera.localPosition = camPos;
            }

            if (Mathf.Abs(controller.height - targetHeight) < 0.01f)
            {
                controller.height = targetHeight;
                currentSpeed = isCrouching ? crouchSpeed : standSpeed;
                isTransitioning = false;
            }
        }
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }
}
