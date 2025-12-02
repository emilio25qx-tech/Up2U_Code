using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerFootstep : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip woodSound;
    public AudioClip carpetSound;

    private CharacterController controller;
    private PlayerMovement movement;

    [Header("Footstep Settings")]
    public float minStepSpeed = 0.1f;
    public float sprintPitch = 1.5f;
    public float crouchPitch = 0.7f;
    public float walkPitch = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();
    }

    public void HandleFootsteps()
    {
        if (!controller.isGrounded)
        {
            if (audioSource.isPlaying) audioSource.Stop();
            return;
        }

        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        if (horizontalVelocity.magnitude > minStepSpeed)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 1.5f))
            {
                if (hit.collider.CompareTag("Wood")) audioSource.clip = woodSound;
                else if (hit.collider.CompareTag("Carpet")) audioSource.clip = carpetSound;
                else audioSource.clip = null;
            }

            if (audioSource.clip != null)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();

                if (movement != null)
                {
                    if (movement.isSprinting) audioSource.pitch = sprintPitch;
                    else if (movement.crouch != null && movement.crouch.IsCrouching()) audioSource.pitch = crouchPitch;
                    else audioSource.pitch = walkPitch;
                }
            }
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }
    }
}
