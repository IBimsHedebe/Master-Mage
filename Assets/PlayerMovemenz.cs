using UnityEngine;
// WICHTIG: Diese Zeile oben hinzufügen!
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement Speeds")]
    public float speed = 12f;
    public float crouchSpeed = 6f;
    private float currentSpeed;

    [Header("Physics")]
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Crouching")]
    public float normalHeight = 2f;
    public float crouchHeight = 1f;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        // Prüfen, ob eine Tastatur angeschlossen ist
        if (Keyboard.current == null) return;

        // Prüfen, ob der Spieler auf dem Boden steht
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // WASD Input über das NEW Input System
        float x = 0f;
        float z = 0f;

        if (Keyboard.current.wKey.isPressed) z = 1f;
        if (Keyboard.current.sKey.isPressed) z = -1f;
        if (Keyboard.current.dKey.isPressed) x = 1f;
        if (Keyboard.current.aKey.isPressed) x = -1f;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Springen (Leertaste / Space)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Ducken (Linke Strg-Taste / Left Control)
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame)
        {
            controller.height = crouchHeight;
            currentSpeed = crouchSpeed;
        }
        if (Keyboard.current.leftCtrlKey.wasReleasedThisFrame)
        {
            controller.height = normalHeight;
            currentSpeed = speed;
        }

        // Gravitation anwenden
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
