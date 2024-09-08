using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5.0f;

    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;


    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.activeSpriteRenderer = this.spriteRendererDown;
    }

    private void Update()
    {
        if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, this.spriteRendererDown);
        }
        else if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, this.spriteRendererUp);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, this.spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, this.spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, this.activeSpriteRenderer);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * speed * Time.fixedDeltaTime;

        this.rigidbody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        this.direction = newDirection;

        this.spriteRendererDown.enabled = spriteRenderer == this.spriteRendererDown;
        this.spriteRendererLeft.enabled = spriteRenderer == this.spriteRendererLeft;
        this.spriteRendererRight.enabled = spriteRenderer == this.spriteRendererRight;
        this.spriteRendererUp.enabled = spriteRenderer == this.spriteRendererUp;
        this.activeSpriteRenderer = spriteRenderer;

        this.activeSpriteRenderer = spriteRenderer;
        this.activeSpriteRenderer.idle = direction == Vector2.zero;

        //Cach hoat dong:

        //if (spriteRenderer == this.spriteRendererDown)
        //{
        //    this.spriteRendererDown.enabled = true;
        //    this.spriteRendererLeft.enabled = false;
        //    this.spriteRendererRight.enabled = false;
        //    this.spriteRendererUp.enabled = false;
        //}
        //else if (spriteRenderer == this.spriteRendererUp)
        //{
        //    this.spriteRendererDown.enabled = false;
        //    this.spriteRendererLeft.enabled = false;
        //    this.spriteRendererRight.enabled = false;
        //    this.spriteRendererUp.enabled = true;
        //}
        //else if (spriteRenderer == this.spriteRendererLeft)
        //{
        //    this.spriteRendererDown.enabled = false;
        //    this.spriteRendererLeft.enabled = true;
        //    this.spriteRendererRight.enabled = false;
        //    this.spriteRendererUp.enabled = false;
        //}
        //else if (spriteRenderer == this.spriteRendererRight)
        //{
        //    this.spriteRendererDown.enabled = false;
        //    this.spriteRendererLeft.enabled = false;
        //    this.spriteRendererRight.enabled = true;
        //    this.spriteRendererUp.enabled = false;
        //}
        //this.activeSpriteRenderer = spriteRenderer;
        //if (this.direction == Vector2.zero)
        //{
        //    this.activeSpriteRenderer.idle = true;
        //}
        //else
        //{
        //    this.activeSpriteRenderer.idle = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        this.enabled = false;
        GetComponent<BombController>().enabled = false;

        this.spriteRendererDown.enabled = false;
        this.spriteRendererUp.enabled = false;
        this.spriteRendererLeft.enabled = false;
        this.spriteRendererRight.enabled = false;
        this.spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        this.gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
}
