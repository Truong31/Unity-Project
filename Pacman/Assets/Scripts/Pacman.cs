using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField]
    private AnimatedSprite pacmanDiedAnimation;
    private Movement movement;
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponent<Movement>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up);
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    public void ResetState()
    {
        this.enabled = true;
        this.circleCollider.enabled = true;
        this.spriteRenderer.enabled = true;
        this.pacmanDiedAnimation.enabled = false;
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }

    public void PacmanDied()
    {
        this.enabled = false;
        this.circleCollider.enabled = false;
        this.spriteRenderer.enabled = false;
        this.movement.enabled = false;
        this.pacmanDiedAnimation.enabled = true;
        this.pacmanDiedAnimation.Restart();
    }

}
