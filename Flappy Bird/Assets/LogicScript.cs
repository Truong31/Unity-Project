using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public Text HighScore;
    public Text yourScore;
    private int highestScore = 0;
    public GameObject gameOverScreen;
    public GameObject pausePanel;
    public Button pauseButton;
    public AudioSource backgroundMusic;

    [ContextMenu("Increase Score")]
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        updateHighScore();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }
    public void pauseGame()
    {
        backgroundMusic.Stop();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        backgroundMusic.Play();
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void addScore( int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void checkForHighScore()
    {
        if(playerScore > highestScore)
        {
            highestScore = playerScore;
            yourScore.text = "High score: " + playerScore.ToString();
            PlayerPrefs.SetInt("HighScore", highestScore);
            PlayerPrefs.Save();

        }
        else
        {
            yourScore.text = "Your score: " + playerScore.ToString();
        }

    }
    void updateHighScore()
    {
        highestScore = PlayerPrefs.GetInt("HighScore");
        HighScore.text = "High Score: " + highestScore.ToString();
    }
    public void restartGame()
    {
        backgroundMusic.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver()
    {
        backgroundMusic.Stop();
        checkForHighScore();
        gameOverScreen.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    

}
