using UnityEngine;

public class CoinDetection : MonoBehaviour
{
    GameObject coin;
    CoinMove coinMove;
    void Start()
    {
        coinMove = GetComponent<CoinMove>();
        coin = gameObject;
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "CoinCollector")
        {
            coinMove.enabled = true;
        }
        if (coll.gameObject.tag == "PlayerBody")
        {
            coinMove.enabled = false;
            TilePooler.Instance.DisableObject(coin, TilePooler.Instance.activeCoins, TilePooler.Instance.disabledCoins);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            Death_Score.amountOfCoins++;
        }
    }
}
