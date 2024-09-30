using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
     FireFlower, giu Mario o trang thai Grown khi sang man moi
     Them sound, UI
     Chuyen doi giua cac man(chuyen doi World va Stage, chet o man nao thi hoi sinh o man do)
     */
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    private bool isMuted = false;
    private bool isPaused = false;

    public AudioClip mariodieSound;
    public AudioClip gameOverSound;
    public AudioClip newStageSound;
    public AudioClip newWorldSound;
    public AudioClip backGroundSond;
    public AudioClip pauseSound;
    public AudioSource audioSource;
    public AudioSource coinSound;
    private void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Muted();
        }else if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                Paused();
            }
            else
            {
                Resume();
            }
        }
    }
    private void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLevel(1, 1);
    }
    public void LoadLevel(int world, int stage)
    {
        audioSource.clip = backGroundSond;
        audioSource.loop = true;
        audioSource.Play();
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");     //Chuoi noi suy
                                                        //$"{world}-{stage}" = "world + "-" + stage"
    }
    
    public void ResetLevel()
    {
        lives--;
        if(lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void NextLevel()
    {
        LoadLevel(world, stage + 1);
        audioSource.clip = newStageSound;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void ResetLevelAfter(float delay)
    {
        audioSource.clip = mariodieSound;
        audioSource.loop = false;
        audioSource.Play();
        Invoke(nameof(ResetLevel), delay);
    }

    private void GameOver()
    {
        Invoke(nameof(NewGame), 4f);
        audioSource.clip = gameOverSound;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void AddCoin()
    {
        coinSound.Play();
        coins++;
        if(coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }

    public void AddLife()
    {
        lives++;
    }

    public void Muted()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
    }

    private void Paused()
    {
        isPaused = true;
        AudioSource.PlayClipAtPoint(pauseSound, Camera.main.transform.position);
        Time.timeScale = 0;
    }

    private void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(pauseSound, Camera.main.transform.position);
    }
    private void PlayPauseSound()
    {
        AudioSource.PlayClipAtPoint(pauseSound, Camera.main.transform.position);
    }
}
