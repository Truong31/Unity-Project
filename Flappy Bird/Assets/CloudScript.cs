using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public GameObject cloud;
    public float hightOffset = 2.5f;
    private float timer;
    public float spawnRate = 2;

    // Start is called before the first frame update
    void Start()
    {
        spawnCLoud();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnCLoud();
            timer = 0;
        }
    }
    public void spawnCLoud()
    {
        float topHieght = transform.position.y + hightOffset;
        float bottomHeight = transform.position.y - hightOffset;
        Instantiate(cloud, new Vector3(transform.position.x, Random.Range(topHieght, bottomHeight), 0), transform.rotation);
    }
}
