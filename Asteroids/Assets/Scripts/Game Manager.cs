using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text liveText;
    public Text scoreText;
    public ParticleSystem explosion;
    public Player player;
    public float respawnTime = 3.0f;
    public int lives = 3;
    public int score = 0;
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if(asteroid.size < 0.75f)
        {
            this.score += 3;
        }else if(asteroid.size < 1.2f)
        {
            this.score += 2;
        }
        else
        {
            this.score += 1;
        }
        scoreText.text = score.ToString();
    }
    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;
        liveText.text = lives.ToString();
        if(this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }
    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollision), 3.0f);
    }
    private void TurnOnCollision()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;
        scoreText.text = score.ToString();
        liveText.text = lives.ToString();

        Invoke(nameof(Respawn), this.respawnTime);
    }
}
