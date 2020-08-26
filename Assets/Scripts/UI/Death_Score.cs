using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death_Score : MonoBehaviour
{
    public Text score;
    public Text deadScore, deadHighScore;
    public Canvas DeathMenu;
    public Animator deathMenuAnim;
    public Text allCoins;
    public Text coinsInRun;
    public static int amountOfCoins;

    private int scoreMultiplier = 1;
    private float scoreCount;

    void Start()
    {
        allCoins.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        PlayerPrefs.GetInt("HighScore", 0);
        amountOfCoins = 0;
    }
    void Update()
    {
        scoreCount = scoreMultiplier * transform.position.z;
        score.text = scoreCount.ToString("0");
        coinsInRun.text = "Coins: " + amountOfCoins.ToString();

    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Obstacle"))
        {
            gameObject.GetComponent<PlayerController>().speed = 0f;
            DeathMenu.gameObject.SetActive(true);
            deadScore.text = "Score: " + scoreCount.ToString("0");
            deadHighScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore").ToString();
            allCoins.text = "All coins: " + PlayerPrefs.GetInt("Coins").ToString();
            if (scoreCount > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", (int)scoreCount);
            }
            Destroy(gameObject.GetComponent<PlayerController>());
        }
    }
    public void OnPlayButton(int i)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);
        deadScore = null;
    }
}
