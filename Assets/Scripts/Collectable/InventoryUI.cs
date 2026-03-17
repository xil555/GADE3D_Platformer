using UnityEngine;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI gemText;

     void Start()
    {
        gemText = GetComponent<TextMeshProUGUI>();  
    }

    public void UpdateGemText(PlayerInventory inventory)
    {
        gemText.text = inventory.noOfCoins.ToString();
    }

}
