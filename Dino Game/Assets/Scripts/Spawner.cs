using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }
    public SpawnableObject[] objects;

    public float minSpawnerRate = 1f;
    public float maxSpawnerRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnerRate, maxSpawnerRate));
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    void Spawn()
    {
        float spawnChance = Random.value;

        foreach(var obj in objects)
        {
            if(spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }
            spawnChance -= obj.spawnChance;
        }
        Invoke(nameof(Spawn), Random.Range(minSpawnerRate, maxSpawnerRate));
    }
}
