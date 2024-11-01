using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    //NOTE: dieu chinh Camera di theo Mario 

    private Transform player;

    public float height = 7f;
    public float undergroundHeight = -9f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 camreraPosition = transform.position;
        camreraPosition.x = Mathf.Max(player.position.x, camreraPosition.x);
        transform.position = camreraPosition;
    }

    public void SetUnderGround(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}
