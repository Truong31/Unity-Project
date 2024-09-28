using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starPower { get; private set; }

    public AudioClip powerUpSound;
    public AudioClip losePowerUpSound;
    public AudioClip bumpSound;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }

    public void Hit()
    {
        if(!dead && !starPower)
        {
            if (big)
            {
                AudioSource.PlayClipAtPoint(losePowerUpSound, transform.position);
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

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        AudioSource.PlayClipAtPoint(powerUpSound, transform.position);

        MarioMovement mario = GetComponent<MarioMovement>();
        mario.maxJumpHeight = 5.5f;

        StartCoroutine(ScaleAnimation());
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
        AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
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
                AudioSource.PlayClipAtPoint(bumpSound, transform.position);
                if(big)
                {
                    if (block.maxHit < 0)
                    {
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
