using System.Collections;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public float timeOfActionX2 = 5f;
    public float timeOfActionMagnet = 5f;
    private float saveTimeOfActionX2, saveTimeOfActionMagnet;
    public GameObject CoinCollector;

    void Start()
    {
        saveTimeOfActionX2 = timeOfActionX2;//create a new variable to save timeOfAction
        saveTimeOfActionMagnet = timeOfActionMagnet;
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("x2"))
        {
            TilePooler.Instance.DisableObject(coll.gameObject, TilePooler.Instance.activePowerUps, TilePooler.Instance.disabledPowerUps);
            if (timeOfActionX2 == saveTimeOfActionX2)//if powerup isn't active
            {
                StartCoroutine(x2());
            }
            timeOfActionX2 = saveTimeOfActionX2;
        }
        if (coll.gameObject.CompareTag("Magnet"))
        {
            TilePooler.Instance.DisableObject(coll.gameObject, TilePooler.Instance.activePowerUps, TilePooler.Instance.disabledPowerUps);
            timeOfActionMagnet = saveTimeOfActionMagnet;
            if (!CoinCollector.activeSelf)
            {
                StartCoroutine(Magnet());
            }
        }
    }
    public IEnumerator x2()
    {
        gameObject.GetComponentInParent<PlayerController>().speed *= 2f;
        while (timeOfActionX2 > 0)
        {
            timeOfActionX2 -= Time.deltaTime;// make a timer
            yield return null;
        }//timeOfAction will be zero
        gameObject.GetComponentInParent<PlayerController>().speed *= 0.5f;
        timeOfActionX2 = saveTimeOfActionX2;//give timeOfAction its initial value stored in saveTimeOfAction
    }
    IEnumerator Magnet()
    {
        while (timeOfActionMagnet > 0)
        {
            CoinCollector.SetActive(true);
            timeOfActionMagnet -= Time.deltaTime;
            yield return null;
        }
        CoinCollector.SetActive(false);
    }
}
