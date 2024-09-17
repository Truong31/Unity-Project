using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MarioMovement movement;

    public Sprite idle;
    public AnimatedSprite run;
    public Sprite jump;
    public Sprite slide;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<MarioMovement>();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;

        if (movement.jumping)
            spriteRenderer.sprite = jump;
        else if(movement.sliding)
            spriteRenderer.sprite = slide;
        else if(!movement.running)
            spriteRenderer.sprite = idle;
    }
}
