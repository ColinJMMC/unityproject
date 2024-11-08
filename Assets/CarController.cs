using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    [Header("Mouse Look Settings")]
    public Transform cameraTransform;
    public float mouseSensitivity = 100f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Verberg de muis tijdens het spelen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Zorg dat de Rigidbody geen rotatie veroorzaakt
        rb.freezeRotation = true;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    private void HandleMovement()
    {
        // WASD-input
        float forward = Input.GetAxis("Vertical"); // W/S of pijltjes omhoog/omlaag
        float turn = Input.GetAxis("Horizontal"); // A/D of pijltjes links/rechts

        // Vooruit/achteruit beweging
        Vector3 move = transform.forward * forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(new Vector3(rb.position.x + move.x, 1f, rb.position.z + move.z)); // Houd Y-hoogte vast

        // Rotatie van de auto (links/rechts draaien)
        float turnAngle = turn * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAngle, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void HandleMouseLook()
    {
        // Muisinput
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Horizontale rotatie van de auto
        transform.Rotate(Vector3.up * mouseX);

        // Horizontale rotatie van de camera (kijkt mee met de auto)
        cameraTransform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
}
