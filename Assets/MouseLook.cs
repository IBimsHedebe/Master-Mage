using UnityEngine;
// WICHTIG: Diese Zeile oben hinzufügen!
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        // Sperrt den Mauszeiger in der Mitte des Bildschirms
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Prüfen, ob eine Maus angeschlossen ist
        if (Mouse.current == null) return;

        // Mausbewegung über das NEW Input System auslesen
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * (mouseSensitivity / 10f) * Time.deltaTime;

        float mouseX = mouseDelta.x;
        float mouseY = mouseDelta.y;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Verhindert Überschlagen

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
