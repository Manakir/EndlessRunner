using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public GameObject CoinCollector;
    public float timeOfAction = 3f;
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Magnet"))
        {
            timeOfAction = 3f;
            if (!CoinCollector.activeSelf)
            {
                StartCoroutine(Magnet());
            }
            Destroy(coll.gameObject);
        }
    }
    IEnumerator Magnet()
    {
        while (timeOfAction > 0)
        {
            CoinCollector.SetActive(true);
            timeOfAction -= Time.deltaTime;
            yield return null;
        }
        CoinCollector.SetActive(false);
    }
}
