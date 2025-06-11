using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGround : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Renderer backGround;

    void Update()
    {
        backGround.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
    }
}
