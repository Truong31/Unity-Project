using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghost;
    public Pacman pacman;

    public Transform pellets;

    public int score { get; private set; }
    public int live { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && this.live <= 0)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetSCore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghost.Length; i++)
        {
            this.ghost[i].gameObject.SetActive(true);
        }
        this.pacman.gameObject.SetActive(true);
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
    }

    private void SetLives(int live)
    {
        this.live = live;
    }

    public void GhostEaten(Ghost ghost)
    {
        SetSCore(this.score + 200);
    }
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.live - 1);

        if(this.live > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }
}
