using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public float jumpForce = 15f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }

            Destroy(gameObject); 
        }
    }


}
