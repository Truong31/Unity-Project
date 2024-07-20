using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float speed = 200.0f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        ResetPosition();
    }
    private void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range(0.5f, 1.0f);
        Vector2 direction = new Vector2(x, y);
        rigidbody.AddForce(direction * this.speed);
    }
    public void addForce(Vector2 force)
    {
        rigidbody.AddForce(force);
    }

    public void ResetPosition()
    {
        rigidbody.position = Vector3.zero;
        rigidbody.velocity = Vector3.zero;

        AddStartingForce();
    }
}
