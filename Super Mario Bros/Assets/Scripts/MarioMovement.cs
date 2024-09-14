using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float moveSpeed = 8.0f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += Vector3.right * 8 * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += Vector3.left * 8 * Time.deltaTime;
        }
    }
}
