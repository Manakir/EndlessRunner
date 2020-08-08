using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private const float Deadzone = 100f;
    public static Swipe Instance { get; set; }
    private bool tap, swipeLeft, swipeRight, swipeDown, swipeUp;
    private Vector2 startTouch, swipeDelta;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;//setting bools
        //inputs
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
        #endregion
        #region Mobile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
        #endregion
        //calculate distance
        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }
        //dead zone
        if(swipeDelta.magnitude > Deadzone)
        {
            //confirmed swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0) { swipeLeft = true; }//down
                else { swipeRight = true; }
            }
            else
            {
                //Up or Down
                if (y < 0) { swipeDown = true; }//right
                else { swipeUp = true; }
            }
            Reset();
        }
    }
	public void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
    }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SwipeUp { get { return swipeUp; } }
        
}
