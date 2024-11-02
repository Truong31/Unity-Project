using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private float inputAxis;
    private Vector2 velocity;

    public float moveSpeed = 8.0f;
    public float maxJumpHeight = 5.0f;
    public float maxJumpTime = 1.0f;

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);        // m/s
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2);     // m/s^2

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    public AudioClip jumpSound;
    public AudioSource audioSource;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        this.rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        jumping = false;
        collider.enabled = true;
        velocity = Vector2.zero;
    }

    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
        
    }
    private void Update()
    {
        HorizontalMovement();

        grounded = rigidbody.CircleCast(Vector2.down);

        if (grounded)
        {
            GroundMovement();
        }
        ApplyGravity();
    }

    //NOTE: xu ly chuyen dong ngang
    private void HorizontalMovement()
    {
        /*NOTE: lay gia tri dau vao(nguoi choi nhap tu ban phim)
                Neu nguoi choi nhan LeftArrow ==> inputAxis = -1
                Neu nguoi choi nhan RightArrow ==> inputAxis = 1
         */

        this.inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
        
        if (rigidbody.CircleCast(Vector2.right * velocity.x))
            velocity.x = 0f;

        if (velocity.x > 0f)
            transform.eulerAngles = Vector3.zero;
        else if (velocity.x < 0f)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
    private void GroundMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2.0f : 1.0f;
        
        velocity.y += gravity * multiplier * Time.deltaTime;

        //NOTE: de phong Mario roi qua nhanh
        velocity.y = Mathf.Max(velocity.y, gravity / 2.0f);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //lay gia tri cua position.x nam giua leftEdge va rightEdge
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);      

        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                jumping = true;
            }

        }
        if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if(transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }

    
}
