using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
public class PlayerInventory : MonoBehaviour
{
    public int noOfCoins {  get; private set; }

    public UnityEvent<PlayerInventory> OnGemCollected;
    public void CoinCollection()
    {
        noOfCoins++;
        OnGemCollected.Invoke(this);
      
    }
}
