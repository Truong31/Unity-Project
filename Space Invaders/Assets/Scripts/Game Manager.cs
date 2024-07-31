using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float Score = 0.0f;
    private int Live = 3;
    public Projectile projectile;
    public Player player;
    public Invader invader;

    public void Scoring()
    {
        this.Score += 10.0f;
        Debug.Log(Score);
    }

    public void Died()
    {
        this.Live--;
        if(Live == 0)
        {
            this.Live = 3;
            this.Score = 0.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}
