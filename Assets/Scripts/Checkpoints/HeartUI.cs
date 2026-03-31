using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public PlayerController player;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        if (player == null || hearts == null || hearts.Length == 0)
        {
            return;
        }

        int maxLives = player.maxLives;
        int currentLives = Mathf.Clamp(player.lives, 0, maxLives);

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxLives)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = i < currentLives ? fullHeart : emptyHeart;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
