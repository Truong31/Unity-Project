using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
   [SerializeField] private float jumpForce = 5f;
    private new Rigidbody2D rigidbody2D;
    private bool isJumping = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.isKinematic = true;
        if (rigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D component is missing on this GameObject.");
        }
    }
    private void Update()
    {
        if (isJumping)
        {
            rigidbody2D.isKinematic = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            rigidbody2D.velocity = Vector2.up * jumpForce;
        }

    }

}
