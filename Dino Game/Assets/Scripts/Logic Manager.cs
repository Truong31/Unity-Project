using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance { get; private set; }
    public float initialSpeed = 5f; // toc ðo game ban ðau
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; } // toc ðo tãng dan theo thoi gian

    private DinoScript player;
    private Spawner spawn;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;
    public GameObject retryScene;
    public GameObject pauseScene;
    public AudioSource gameOverSound;

    private float score;
    private void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        player = FindObjectOfType<DinoScript>();
        spawn = FindObjectOfType<Spawner>();
        NewGame();
    }
    public void NewGame()
    {
        Time.timeScale = 1;
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach(var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialSpeed;
        enabled = true;
        score = 0f;

        player.gameObject.SetActive(true);
        spawn.gameObject.SetActive(true);
        retryScene.SetActive(false);
        pauseScene.SetActive(false);
        gameOverSound.Stop();
        highScore();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseScene.SetActive(true);
        }
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    public void resume()
    {
        pauseScene.SetActive(false);
        Time.timeScale = 1;
    }

    public void exit()
    {
        Application.Quit();
    }
    public void gameOver()
    {
        gameOverSound.Play();
        gameSpeed = 0f;
        enabled = false;
        player.gameObject.SetActive(false);
        spawn.gameObject.SetActive(false);
        retryScene.SetActive(true);
        highScore();
    }
    private void highScore()
    {
        float hiSCore = PlayerPrefs.GetFloat("High Score", 0f);
        if (hiSCore < score)
        {
            hiSCore = score;
            PlayerPrefs.SetFloat("High Score", hiSCore);
        }
        hiScoreText.text = Mathf.FloorToInt(hiSCore).ToString("D5");
    }
}
