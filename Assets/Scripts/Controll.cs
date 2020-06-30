using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
    public float forwardSpeed;
   void Update()
    {
        transform.Translate(transform.forward * forwardSpeed * Time.deltaTime);
    }
}
