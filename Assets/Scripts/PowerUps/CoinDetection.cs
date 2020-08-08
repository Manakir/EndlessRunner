    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDetection : MonoBehaviour
{
    CoinMove coinMove;
    void Start()
    {
        coinMove = gameObject.GetComponent<CoinMove>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "CoinCollector")
        {
            coinMove.enabled = true;
        }
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
