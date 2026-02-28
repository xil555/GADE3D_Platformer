using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputController playerInputController;
    private GroundController groundController;
    private Rigidbody _rigidbody;

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float rotationSpeed = 10f; // NEW

    private bool jumpTriggered;

    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
        _rigidbody = GetComponent<Rigidbody>();
        groundController = GetComponent<GroundController>();

        playerInputController.OnJumpedBttonPressed += JumpButtonPressed;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(
            playerInputController.MovementInputVector.x,
            0,
            playerInputController.MovementInputVector.y
        );

        Vector3 velocity = moveDirection * speed;
        velocity.y = _rigidbody.linearVelocity.y;

        if (jumpTriggered)
        {
            velocity.y = jumpSpeed;
            jumpTriggered = false;
        }

        _rigidbody.linearVelocity = velocity;

        // ✅ Rotate player toward movement direction
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
