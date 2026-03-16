using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public float shieldDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShield shield = other.GetComponent<PlayerShield>();

            if (shield != null)
            {
                shield.ActivateShield(shieldDuration);
            }

            Destroy(gameObject); // pickup disappears
        }
    }
}
