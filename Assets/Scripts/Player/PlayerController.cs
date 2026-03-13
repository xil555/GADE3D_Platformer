using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputController playerInputController;
    private GroundController groundController;
    private Rigidbody _rigidbody;

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float rotationSpeed = 10f;

    // This will be the real camera that Cinemachine controls
    [SerializeField] private Transform cameraTransform;

    private bool jumpTriggered;

    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody>();
        groundController = GetComponent<GroundController>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        playerInputController.OnJumpedBttonPressed += JumpButtonPressed;
    }

    private void FixedUpdate()
    {
        Vector2 input = playerInputController.MovementInputVector;

        Vector3 moveDirection;
        if (cameraTransform != null)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            moveDirection = camForward * input.y + camRight * input.x;
        }
        else
        {
            moveDirection = new Vector3(input.x, 0f, input.y);
        }

        Vector3 velocity = moveDirection * speed;
        velocity.y = _rigidbody.linearVelocity.y;

        if (jumpTriggered)
        {
            velocity.y = jumpSpeed;
            jumpTriggered = false;
        }

        _rigidbody.linearVelocity = velocity;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rigidbody.MoveRotation(
                Quaternion.Slerp(_rigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime)
            );
        }
    }

    private void JumpButtonPressed()
    {
        if (groundController.IsGrounded)
        {
            jumpTriggered = true;
        }
    }
}
