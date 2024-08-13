using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public int points = 200;
    public GhostChase chase { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public Movement movement { get; private set; }
    public GhostBehavior initialBehavior;
    public Transform target;

    private void Awake()
    {
        this.chase = GetComponent<GhostChase>();
        this.home = GetComponent<GhostHome>();
        this.movement = GetComponent<Movement>();
        this.frightened = GetComponent<GhostFrightened>();
        this.scatter = GetComponent<GhostScatter>();
    }
    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        if(this.home != this.initialBehavior)
        {
            this.home.Disable();
        }
        if(this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
