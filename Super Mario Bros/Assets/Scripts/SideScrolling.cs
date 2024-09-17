using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

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
}
