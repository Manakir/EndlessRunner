using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeDown, swipeUp;
    private Vector2 startTouch, swipeDelta;
    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
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
        if(Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.touches[0].position;
            }
          /*  else if ()
            {

            }*/     
        }
        #endregion
    }
	private void Reset()
    {
        startTouch = swipeDelta =Vector2.zero;
    }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipDown { get { return swipeDown; } }
    public bool SwipeUp { get { return swipeUp; } }

}
