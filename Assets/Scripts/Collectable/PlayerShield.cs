using UnityEngine;
using TMPro;
public class PlayerShield : MonoBehaviour
{
    public bool shieldActive = false;
    public float shieldTimer;

    public TextMeshProUGUI shieldTimerText;

    void Update()
    {
        if (shieldActive)
        {
            shieldTimer -= Time.deltaTime;

            shieldTimerText.text = "Shield: " + Mathf.Ceil(shieldTimer);

            if (shieldTimer <= 0)
            {
                shieldActive = false;
                shieldTimerText.text = "";
            }
        }
    }

    public void ActivateShield(float duration)
    {
        shieldActive = true;
        shieldTimer = duration;
    }
}
