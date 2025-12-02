using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Camera playerCamera;

    private PlayerMovement movement;
    private PlayerLook look;
    private PlayerCrouch crouch;
    private PlayerStamina stamina;
    private PlayerFootstep footstep;
    private PlayerInteraction interact;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        crouch = GetComponent<PlayerCrouch>();
        stamina = GetComponent<PlayerStamina>();
        footstep = GetComponent<PlayerFootstep>();
        interact = GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
        {
            return;
        }

        movement?.HandleMovement();
        look?.HandleLook();
        crouch?.HandleCrouch();
        stamina?.HandleStamina();
        footstep?.HandleFootsteps();
    }
}
