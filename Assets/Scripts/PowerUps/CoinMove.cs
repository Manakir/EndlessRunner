using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public float coinSpeed = 15f;
    private Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, coinSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "PlayerBubble")
        {
            Destroy(gameObject);
        }
    }
}
