using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int noOfCoins = 0;

    [Header("Gem counter (pick one)")]
    [Tooltip("Drag your TextMeshProUGUI gem number here — same GameObject as Player is fine, or any UI text.")]
    [SerializeField] private TextMeshProUGUI gemCounterText;

    [Tooltip("Optional: only if you use InventoryUI instead of Gem Counter Text above.")]
    [SerializeField] private InventoryUI gemUI;

    private InventoryUI inventoryUI;

    private void Start()
    {
        EnsureUI();
        RefreshUI();
    }

    public void CoinCollection()
    {
        noOfCoins++;
        RefreshUI();
    }

    public void ResetCoins()
    {
        noOfCoins = 0;
        EnsureUI();
        RefreshUI();
    }

    private void EnsureUI()
    {
        if (gemCounterText != null)
        {
            return;
        }

        if (gemUI != null)
        {
            inventoryUI = gemUI;
            return;
        }

        if (inventoryUI == null)
        {
            inventoryUI = Object.FindAnyObjectByType<InventoryUI>();
        }

        if (inventoryUI == null)
        {
            InventoryUI[] all = Resources.FindObjectsOfTypeAll<InventoryUI>();
            for (int i = 0; i < all.Length; i++)
            {
                InventoryUI ui = all[i];
                if (ui != null && ui.gameObject.scene.IsValid())
                {
                    inventoryUI = ui;
                    break;
                }
            }
        }
    }

    private void RefreshUI()
    {
        if (gemCounterText != null)
        {
            gemCounterText.text = noOfCoins.ToString();
            return;
        }

        EnsureUI();
        if (inventoryUI != null)
        {
            inventoryUI.UpdateGemText(this);
        }
    }
}
