using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //NOTE: xu ly hieu ung vo gach khi bi Big Mario va cham

    public GameObject brickBrokenPrefabs;
    public float explosionForce = 10;
    public void BreakBrickAnimation()
    {
        GameObject brokenBrick = Instantiate(brickBrokenPrefabs, transform.position, Quaternion.identity);

        Rigidbody2D[] pieces = brokenBrick.GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D rigidbody in pieces)
        {
            Vector2 explosionDirection = rigidbody.transform.position - transform.position;
            rigidbody.AddForce(explosionDirection.normalized * explosionForce, ForceMode2D.Impulse);
            Destroy(rigidbody.gameObject, 3.0f);
        }
        Destroy(gameObject);
            
    }
}
