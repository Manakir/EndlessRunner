using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Controller : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLine = 1;// 0 - left side, 1 - center, 2 - right side
    public float laneDistance = 5;
    private Vector2 startPosition, endPosition;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        direction.z = forwardSpeed;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)// swipe began
        {
            startPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) // swipe ended
        {
            endPosition = Input.GetTouch(0).position;
        }
        if (endPosition.x - startPosition.x > 0 && desiredLine != 2)//right borders
        {
            desiredLine++;
        }
        else if(endPosition.x - startPosition.x > 0 && desiredLine != 0)//left borders
        {
            desiredLine--;
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLine == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLine == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        transform.position = targetPosition;
       //transform.position = Vector3.Lerp(transform.position, targetPosition, 80*Time.deltaTime);
    }
    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
}
