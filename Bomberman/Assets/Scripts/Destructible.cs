using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1.0f;

    [Range(0.0f, 1.0f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;

    private void Start()
    {
        Destroy(this.gameObject, this.destructionTime);
    }

    //Random cac Item sau khi pha vo khoi gach
    private void OnDestroy()
    {
        if(this.spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, this.spawnableItems.Length);
            Instantiate(this.spawnableItems[randomIndex], this.transform.position, Quaternion.identity);
        }
    }
}
