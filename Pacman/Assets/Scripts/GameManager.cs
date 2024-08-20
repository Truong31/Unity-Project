using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghost;
    public Pacman pacman;
    private bool isWaiting = true;

    public Transform pellets;
    public int ghostMultiplier { get; private set; } = 1;

    public int score { get; private set; }
    public int live { get; private set; }

    public Text ReadyText;
    public Text GameOverText;
    public Text ScoreText;
    public Text LiveText;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (this.live <= 0)
            {
                NewGame();
            }
            else if (this.isWaiting)
            {
                StartGame();
            }

        }
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        this.ReadyText.enabled = false;
        this.isWaiting = false;
    }
    private void NewGame()
    {
        this.GameOverText.enabled = false;
        this.ReadyText.enabled = true;
        this.isWaiting = true;
        this.ScoreText.text = 0 + "";
        this.LiveText.text = 3 + "x";
        SetSCore(0);
        SetLives(3);
        NewRound();
        Time.timeScale = 0;
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
             pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghost.Length; i++)
        {
            this.ghost[i].ResetState();
        }
        this.pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghost.Length; i++)
        {
            this.ghost[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }

    private void SetSCore(int score)
    {
        this.score = score;
        this.ScoreText.text = this.score.ToString();
    }

    private void SetLives(int live)
    {
        this.live = live;
        this.LiveText.text = this.live + "x";
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetSCore(this.score + points);
        this.ghostMultiplier++;
    }
    public void PacmanEaten()
    {
        this.pacman.PacmanDied();
        for (int i = 0; i < this.ghost.Length; i++)
        {
            this.ghost[i].gameObject.SetActive(false);
        }
        SetLives(this.live - 1);
        if (this.live > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
            this.GameOverText.enabled = true;
        }
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetSCore(this.score + pellet.points);

        if (!HasRemainingPellet())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghost.Length; i++)
        {
            this.ghost[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);

    }

    private bool HasRemainingPellet()
    {
        foreach(Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }

}
