using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 120f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // Keep object fixed in place
        transform.position = startPosition;

        // Rotate around chosen axis
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
