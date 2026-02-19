using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputController playerInputController;
    private GroundController groundController;
    private Rigidbody _rigidbody;

    [SerializeField]
      private float speed;

    [SerializeField]
    private float jumpSpeed;
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
        Vector3 velocity = new Vector3(playerInputController.MovementInputVector.x, 
            0,
            playerInputController.MovementInputVector.y)  *speed ;
       
       velocity.y = _rigidbody.linearVelocity.y;

        if(jumpTriggered )
        {
            velocity.y = jumpSpeed;
            jumpTriggered = false;
        }

        _rigidbody.linearVelocity = velocity;
    }


    private void JumpButtonPressed()
    {

        if (groundController.IsGrounded)
        {
            jumpTriggered = true;
        }
    }
}
