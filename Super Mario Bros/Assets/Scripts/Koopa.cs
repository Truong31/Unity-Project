using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;
    private bool shelled;
    private bool pushed;

    public AudioSource kickSound;

    private void Update()
    {
        SpriteDirection();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            PlayerState player = collision.gameObject.GetComponent<PlayerState>();
            if (player.starPower)
            {
                Hit();
            }
            else if(collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(shelled && collision.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                PlayerState player = collision.GetComponent<PlayerState>();
                if (player.starPower)
                {
                    Hit();
                }else
                {
                    player.Hit();
                }
            }
        }else if (!shelled && collision.gameObject.layer == LayerMask.NameToLayer("Shell")){
            Hit();
        }
    }

    private void EnterShell()
    {
        shelled = true;
        kickSound.Play();

        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;
        kickSound.Play();

        GetComponent<Rigidbody2D>().isKinematic = false;
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        kickSound.Play();
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed)
        {
            Destroy(gameObject);
        }
    }

    private void SpriteDirection()
    {
        EntityMovement move = GetComponent<EntityMovement>();
        if(move.direction.x < 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}
