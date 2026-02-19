using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GroundController : MonoBehaviour
{

    [SerializeField]
    private float groundDistanceTolerance;


    [SerializeField]
    private LayerMask groundLayerMask;

    private CapsuleCollider capsuleCollider;

    public bool IsGrounded { get; private set; }

    public float? DistanceToGround { get; private set; }


    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        float sphereCastRadius = capsuleCollider.radius -0.1f;

        Vector3 sphereCastOrigin = transform.position + new Vector3(0, capsuleCollider.radius, 0);

        bool isGroundedBelow = Physics.SphereCast(sphereCastOrigin, sphereCastRadius, Vector3.down, out RaycastHit hitInfo, 1000, groundLayerMask, QueryTriggerInteraction.Ignore);


        if (isGroundedBelow)
        {
            DistanceToGround = transform.position.y - hitInfo.point.y;
        }
        else
        {
            DistanceToGround = null;
        }

        IsGrounded = isGroundedBelow && DistanceToGround <= groundDistanceTolerance;
    }
}
