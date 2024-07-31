using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;

    public float animationTime = 1.0f;
    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    public System.Action killed;

    public GameManager gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }
    private void AnimateSprite()
    {
        animationFrame++;
        if(animationFrame >= this.animationSprites.Length)
        {
            animationFrame = 0;
        }
        spriteRenderer.sprite = this.animationSprites[animationFrame];

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            FindObjectOfType<GameManager>().Scoring();
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
