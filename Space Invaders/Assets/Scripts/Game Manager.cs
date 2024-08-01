using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float Score = 0.0f;
    public int Lives = 3;

    public TextMeshProUGUI scoreText;

    public void Scoring()
    {
        this.Score += 10.0f;
        scoreText.text = this.Score.ToString();
    }

    public void hitMysteryShip()
    {
        this.Score += 50.0f;
        scoreText.text = this.Score.ToString();
    }

    public void Died()
    {
        this.Lives--;
        FindObjectOfType<PlayerLives>().LostLive();
        if(this.Lives == 0)
        {
            this.Lives = 3;
            this.Score = 0.0f;
            scoreText.text = 0 + "";
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}