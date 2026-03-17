using UnityEngine;

public class Coins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null )
        {
            inventory.CoinCollection();
            gameObject.SetActive(false);
        }
    }
}
