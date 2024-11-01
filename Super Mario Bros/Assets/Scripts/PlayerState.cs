using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //NOTE: Xử lý các hiệu ứng Mario khi thu thập các Item sức mạnh

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

    //NOTE: Xử lý khi Mario va chạm với vật thể khác(Enemy)
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

    //NOTE: Bật Script deathAnimation nếu Mario chết
    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.ResetLevelAfter(3.0f);
    }

    //NOTE: Xử lý thu nhỏ Mario
    private void Shrink()
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;
        GameManager.Instance.isMarioBig(false);

        capsuleCollider.size = new Vector2(0.8f, 0.9f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        MarioMovement mario = GetComponent<MarioMovement>();
        mario.maxJumpHeight = 5f;

        StartCoroutine(ScaleAnimation());
    }

    //NOTE: Xử lý phóng to Mario
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
    
    //NOTE: Tạo hiệu ứng nhấp nháy khi Mario thu nhỏ hoặc biến to
    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;     //Thời gian trôi qua (bộ đếm thời gian)
        float duration = 0.5f;      //Lượng thời gian hoạt động

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if(Time.frameCount % 6 == 0)        //Cứ mỗi 6 khung hình thì if hoạt động (các ảnh Big và Small sẽ được bật thay phiên nhau)
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

    //NOTE: Gọi hàm này khi Mario ăn được StarPower
    public void StarPower(float duration = 10f)
    {
        audioSource.clip = powerUpSound;
        audioSource.Play();
        StartCoroutine(StarPowerAnimation(duration));
    }

    //NOTE: Tạo hiệu ứng đổi màu khi Mario ăn được StarPower
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

    /*
     *NOTE: Xử lý khi Mario va chạm Brick
     * Nếu Mario đang ở trạng thái Big ==> khi va chạm Brick sẽ xuất hiện hiệu ứng Broken(được viết trong Script Brick)
     * Nếu Mario đang ở trạng thái nhỏ, hoặc khối Brick có chứa Item ==> không có hiệu ứng
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();
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
                        brick.BreakBrickAnimation();
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
