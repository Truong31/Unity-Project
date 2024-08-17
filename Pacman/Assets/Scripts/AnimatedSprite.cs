using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites = new Sprite[0];
    public float animationTime = 0.25f;
    public int animationFrame { get; private set; }
    public bool loop = true;


    private void OnEnable()
    {
        this.spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        this.spriteRenderer.enabled = false;
    }
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);   //(..., thoi gian thuc hien lan dau tien,
                                                                                    // thoi gian giua cac lan phuong thuc duoc goi lai)
    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled)
        {
            return;
        }
        this.animationFrame++;
        if(this.animationFrame >= this.sprites.Length)
        {
            animationFrame = 0;
        }
        if(this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[animationFrame];
        }
    }
    public void Restart()
    {
        this.animationFrame = -1;
        Advance();
    }

}
