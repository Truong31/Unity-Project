using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }

    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starPower { get; private set; }
    private bool skipGrowEffect = false;

    public AudioClip powerUpSound;
    public AudioClip losePowerUpSound;
    public AudioClip bumpSound;
    public AudioClip breakBickSound;
    public AudioSource audioSource;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    private void Start()
    {
        if (GameManager.Instance.isBig)
        {
            skipGrowEffect = true;
            Grow();
            skipGrowEffect = false;
        }
    }
    public void Hit()
    {
        if(!dead && !starPower)
        {
            if (big)
            {
                audioSource.clip = losePowerUpSound;
                audioSource.Play();
                Shrink();
            }
            else
            {
                Death();
            }
        }
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevelAfter(3.0f);
    }


    private void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;
        GameManager.Instance.isMarioBig(false);

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        MarioMovement mario = GetComponent<MarioMovement>();
        mario.maxJumpHeight = 5f;

        StartCoroutine(ScaleAnimation());
    }

    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;
        GameManager.Instance.isMarioBig(true);

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        if (!skipGrowEffect)
        {
            audioSource.clip = powerUpSound;
            audioSource.Play();

            MarioMovement mario = GetComponent<MarioMovement>();
            mario.maxJumpHeight = 5.5f;

            StartCoroutine(ScaleAnimation());
        }
        else
        {
            MarioMovement mario = GetComponent<MarioMovement>();
            mario.maxJumpHeight = 5.5f;
        }
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 6 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }
            yield return null;
        }

        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void StarPower(float duration = 10f)
    {
        audioSource.clip = powerUpSound;
        audioSource.Play();
        StartCoroutine(StarPowerAnimation(duration));
    }

    private IEnumerator StarPowerAnimation(float duration)
    {
        starPower = true;
        float elapsed = 0f;
        
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 6 == 0)
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starPower = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            SpriteRenderer sprite = collision.gameObject.GetComponent<SpriteRenderer>();
            BlockHit block = collision.gameObject.GetComponent<BlockHit>();
            if (collision.transform.DotTest(transform, Vector2.down)){
                audioSource.clip = bumpSound;
                audioSource.Play();
                if (big)
                {
                    if (block.maxHit < 0)
                    {
                        audioSource.clip = breakBickSound;
                        audioSource.Play();
                        brick.BreakBrickAnimation(collision.gameObject);
                    }
                    else if(block.maxHit == 0)
                    {
                        collision.gameObject.tag = "Untagged";
                    }
                }
            }
        }
    }
}
