using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoScript : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    public AudioSource jumpSound;
    // Start is called before the first frame update
    void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;
        if (character.isGrounded)
        {
            direction = Vector3.down;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpSound.Play();
                direction = Vector3.up * jumpForce;
            }
        }
        character.Move(direction * Time.deltaTime);
    }
    private void OnEnable()
    {
        direction = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            LogicManager.Instance.gameOver();
        }
    }
}
