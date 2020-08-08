using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
   void LateUpdate()
    {
        transform.position = new Vector3(0, 1, transform.position.z);
    }
}
