using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    public Sprite idleSprite;       //Sprite khi nhan vat dung yen
    public Sprite[] animationSprites;

    private SpriteRenderer spriteRenderer;
    private int currentSprite = 0;
    public float animationTime = 0.25f;

    public bool loop = true;
    public bool idle = true;        //Check xem nhan vat co dang dung yen

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), this.animationTime, this.animationTime);
    }

    private void OnEnable()
    {
        this.spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        this.spriteRenderer.enabled = false;
    }
    private void NextFrame()
    {
        this.currentSprite++;
        if(loop && this.currentSprite >= this.animationSprites.Length)
        {
            this.currentSprite = 0;
        }
        if (idle)
        {
            this.spriteRenderer.sprite = this.idleSprite;
        }
        else if(this.currentSprite < this.animationSprites.Length && this.currentSprite >= 0)
        {
            this.spriteRenderer.sprite = this.animationSprites[currentSprite];
        }
    }
}
