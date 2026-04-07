using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public float moveDistance = 3f;   // How far it moves left/right
    public float speed = 2f;          // Movement speed

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // PingPong moves back and forth automatically
        float movement = Mathf.PingPong(Time.time * speed, moveDistance * 2) - moveDistance;

        // Apply movement on X axis (side-to-side)
        transform.position = startPosition + new Vector3(0f, 0f, movement);
    }
}
