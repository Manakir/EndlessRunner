using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death_Score : MonoBehaviour
{
    public Text score, highScore;
    public Text deadScore, deadHighScore;
    public Canvas DeathMenu;
    public Animator deathMenuAnim;

    private int scoreMultiplier = 1;
    private float scoreCount = 0;

    public float speed = 10f;
    public float maxSpeed = 15f;
    public int distance = 30;

    public float timeOfAction = 5f;
    private float saveTimeOfAction;
    PlayerController playerController;
    void Start()
    {
        playerController = this.gameObject.GetComponent<PlayerController>();//reference of another script
        saveTimeOfAction = timeOfAction;//create a new variable to save timeOfAction
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    void Update()
    {
        if (transform.position.z > distance && speed < maxSpeed)//if we have passed some distance add speed and substract lane change time
        {
            if (playerController.laneChangeTime > 0.35f)
            {
                playerController.laneChangeTime -= 0.01f;
            }
            distance += 30;
            speed += 0.2f;
        }
        transform.position += Vector3.forward * speed * Time.deltaTime; // const movement by z-axis
        scoreCount = scoreMultiplier * transform.position.z;
        score.text = scoreCount.ToString("0");
        if (scoreCount > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", (int)scoreCount);
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Obstacle"))
        {
            speed = 0f;
            DeathMenu.gameObject.SetActive(true);
            deadScore.text = "Score: " + scoreCount.ToString("0");
            deadHighScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore").ToString();
            Destroy(playerController);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("x2"))
        {
            Destroy(coll.gameObject);
            if (timeOfAction == saveTimeOfAction)//if powerup isn't active
            {
                StartCoroutine(x2());
            }
            timeOfAction = saveTimeOfAction;
        }
    }
    public IEnumerator x2()
    {
        speed *= 2;
        while (timeOfAction > 0)
        {
            timeOfAction -= Time.deltaTime;// make a timer
            yield return null;
        }//timeOfAction will be zero
        speed *= 0.5f;
        timeOfAction = saveTimeOfAction;//give timeOfAction its initial value stored in saveTimeOfAction
    }
    public void OnPlayButton(int i)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);
        deadHighScore = null;
        deadScore = null;
    }
}
