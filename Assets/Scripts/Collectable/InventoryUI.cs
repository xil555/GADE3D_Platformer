using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI gemText;

    private void Awake()
    {
        ResolveGemText();
    }

    private void Start()
    {
        ResolveGemText();
    }

    private void ResolveGemText()
    {
        if (gemText == null)
        {
            gemText = GetComponent<TextMeshProUGUI>();
        }

        if (gemText == null)
        {
            gemText = GetComponentInChildren<TextMeshProUGUI>(true);
        }
    }

    public void UpdateGemText(PlayerInventory inventory)
    {
        ResolveGemText();

        if (gemText != null && inventory != null)
        {
            gemText.text = inventory.noOfCoins.ToString();
        }
    }
}
