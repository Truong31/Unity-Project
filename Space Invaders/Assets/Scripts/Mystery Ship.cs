using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    private bool MovingRight = true;
    private void Update()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (MovingRight)
        {
            this.transform.position += Vector3.right * 5.0f * Time.deltaTime;
            if(this.transform.position.x > (rightEdge.x - 1.0f))
            {
                MovingRight = false;
            }
        }
        else{
            this.transform.position += Vector3.left * 5.0f * Time.deltaTime;
            if(this.transform.position.x < (leftEdge.x + 1.0f))
            {
                MovingRight = true;
            }
        }
    }
}
