using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 distance;
    void Update()
    {
        transform.position = playerTransform.position + distance;
    }
}
