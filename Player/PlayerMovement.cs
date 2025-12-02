using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float gravity = 10f;

    private CharacterController controller;
    private Vector3 moveDir = Vector3.zero;

    public bool isSprinting;

    [Header("Crouch Reference")]
    public PlayerCrouch crouch;

    [Header("Stamina Reference")]
    public PlayerStamina stamina;

    private bool canSprint = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void HandleMovement()
    {
        if (controller.isGrounded)
        {
            float speed = crouch != null ? crouch.GetCurrentSpeed() : walkSpeed;

            if (stamina != null)
            {
                if (stamina.currentStamina <= 0f)
                {
                    canSprint = false;
                    stamina.currentStamina = 0f;
                }
                else if (Mathf.Approximately(stamina.currentStamina, stamina.maxStamina))
                {
                    canSprint = true;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift)
                && (crouch == null || !crouch.IsCrouching())
                && canSprint)
            {
                speed = runSpeed;
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical");
            Vector3 right = transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal");

            moveDir = (forward + right) * speed;
        }

        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);
    }
}
