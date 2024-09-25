using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject brickBrokenPrefabs;
    public float explosionForce = 10;
    public void BreakBrickAnimation(GameObject brick)
    {
        GameObject brokenBrick = Instantiate(brickBrokenPrefabs, brick.transform.position, Quaternion.identity);

        Rigidbody2D[] pieces = brokenBrick.GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D rigidbody in pieces)
        {
            Vector2 explosionDirection = rigidbody.transform.position - brick.transform.position;
            float distance = explosionDirection.magnitude;
            if(distance <= 2)
            {
                float force = explosionForce * (1 - (distance / 2f));
                rigidbody.AddForce(explosionDirection.normalized * force, ForceMode2D.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
