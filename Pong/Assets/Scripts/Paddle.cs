using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    protected Rigidbody2D rigidBody2D;
    public float Speed = 10.0f;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
}
