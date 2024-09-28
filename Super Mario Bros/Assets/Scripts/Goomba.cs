using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    public AudioClip kickSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerState player = collision.gameObject.GetComponent<PlayerState>();
            if (player.starPower)
            {
                Hit();
            }else if (collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
            else
            {
                player.Hit();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }
    private void Flatten()
    {
        AudioSource.PlayClipAtPoint(kickSound, transform.position);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;       
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    private void Hit()
    {
        AudioSource.PlayClipAtPoint(kickSound, transform.position);
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
