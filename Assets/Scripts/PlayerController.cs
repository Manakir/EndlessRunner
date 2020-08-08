using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Slide")]
    private float slideTime = 1f;
    private float slideTimer = 0f;
    [Header("Jump")]
    public float ForceConst = 10;
    private Rigidbody Rigidbody;
    public float distance = 1f;
    [Header("Player")]
    public Transform player;
    public float maxSpeed; 
    public float laneChangeTime = 0.5f;
    public CapsuleCollider PlayerCollider;
    [Header("Lanes")]
    public int laneCount;
    public float laneWidth;

    private Vector3 playerStartPosition;
    private float currentLane;
    private bool isMoving;
    private bool isSliding = false;
    private void Awake()
    {
        playerStartPosition = player.transform.position;
        PlayerCollider = GetComponent<CapsuleCollider>();
        Reset();
    }
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (Swipe.Instance.SwipeLeft) { Left(); }
        if (Swipe.Instance.SwipeRight) { Right(); }
        if (Swipe.Instance.SwipeUp && IsGrounded())
        {
            slideTimer = 0;
            Rigidbody.AddForce(0, ForceConst, 0, ForceMode.Impulse); 
        }
        if (Swipe.Instance.SwipeDown && !IsGrounded()) { Rigidbody.AddForce(0, -ForceConst, 0, ForceMode.Impulse); }
        else if (Swipe.Instance.SwipeDown && !isSliding && IsGrounded())
        {
            isSliding = true;
            StartCoroutine(Slide());
        }
    }

    public bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);//initialize a ray down to the platform
        return Physics.Raycast(ray, distance); // check if this ray can reach the platform
    }
    public void Left()
    {
        if (!isMoving)
            if (currentLane > 0)//condition of borders
            {
                StartCoroutine(MovePlayer(player.position - player.transform.right * laneWidth));
                currentLane--;
            }
    }
    public void Right()
    {
        if (!isMoving)
            if (currentLane < laneCount - 1)
            {
                StartCoroutine(MovePlayer(player.position + player.transform.right * laneWidth));
                currentLane++;
            }
    }
    public IEnumerator MovePlayer(Vector3 target)
    {
        isMoving = true;
        Vector3 relativeTarget = target - player.transform.position;

        float startTime = Time.time;
        float t = 0;
        while (Time.time < (startTime + laneChangeTime))
        {
            yield return null;
            player.transform.position -= Vector3.Lerp(Vector3.zero, relativeTarget, t);
            t = (Time.time - startTime) / laneChangeTime;
            player.transform.position += Vector3.Lerp(Vector3.zero, relativeTarget, t);
        }

        isMoving = false;
        yield return new WaitForSeconds(0.1f);
    }
    public IEnumerator Slide()
    {
        slideTimer = slideTime;//make timer
        while (slideTimer > 0) 
        {// change collider 
            player.localScale = new Vector3(1f, 0.5f, 1f);
            PlayerCollider.center = new Vector3(0, -0.5f, 0);
            PlayerCollider.height = 0.5f;
            slideTimer -= Time.deltaTime;
            yield return null;
        }//change Scale
        player.localScale = new Vector3(1f, 1f, 1f);
        PlayerCollider.center = new Vector3(0, 0, 0);
        PlayerCollider.height = 2;
        isSliding = false;
    }
    public void Reset()
    {
        player.gameObject.SetActive(true);
        player.transform.position = playerStartPosition;
        currentLane = laneCount * 0.5f;
        StopAllCoroutines();
        isMoving = false;
    }
}
