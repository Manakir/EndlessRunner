using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    GameObject coin;
    void OnEnable()
    {
        coin = TilePooler.Instance.EnableObject(transform, TilePooler.Instance.activeCoins, TilePooler.Instance.disabledCoins);
    }

    void OnDisable()
    {
        if (TilePooler.Instance.activeCoins.Contains(coin))
        {
            TilePooler.Instance.DisableObject(coin, TilePooler.Instance.activeCoins, TilePooler.Instance.disabledCoins);
        }
    }
}
