using UnityEngine;

public class MotorFollower : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform playerTransform; // De speler die gevolgd wordt

    [Header("Movement Settings")]
    public float followSpeed = 5f; // Snelheid waarmee de motor beweegt
    public float rotationSpeed = 5f; // Snelheid waarmee de motor draait om naar de speler te kijken
    public float stoppingDistance = 2f; // Hoe dichtbij de motor de speler stopt

    [Header("Height Settings")]
    public float fixedHeight = 1f; // De vaste hoogte waarop de motor blijft

    [Header("Model Settings")]
    public Vector3 rotationOffset = Vector3.zero; // Correctie voor een gedraaid model

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // Bereken de richting naar de speler
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0f; // Houd de motor horizontaal (negeer hoogteverschillen)

        // Stop met bewegen als de motor binnen de stoppingDistance is
        if (direction.magnitude > stoppingDistance)
        {
            // Beweeg naar de speler toe
            Vector3 moveDirection = direction.normalized * followSpeed * Time.deltaTime;
            transform.position += moveDirection;
        }

        // Draai naar de speler met een offset
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(rotationOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Zorg dat de motor op de vaste hoogte blijft
        transform.position = new Vector3(transform.position.x, fixedHeight, transform.position.z);
    }
}
