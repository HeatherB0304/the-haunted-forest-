using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    float score;
    
    void Start()
    {
        float tempScore = PlayerPrefs.GetFloat("highScore");
        highScoreText.text = tempScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score = score + 1f * Time.deltaTime;
        scoreText.text = score.ToString();

        if (Input.GetKeyDown("p"))
        {
            ResetHighscore();
        }
    }

    public void SetHighScore()
    {
        float tempHighScore = PlayerPrefs.GetFloat("highScore");

        if(score > tempHighScore)
        {
            PlayerPrefs.SetFloat("highScore", score);
        }

    }
    public void ResetHighscore()
    {
        PlayerPrefs.SetFloat("highScore", 0f);
        float tempScore = PlayerPrefs.GetFloat("highScore");
        highScoreText.text = tempScore.ToString();
    }
}
