using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefab;
    float timer = 0f;
    public float spawnInterval = 2f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnInterval){
            SpawnRandomEnemy();
            timer = 0f;
        }
    }

    void SpawnRandomEnemy()
    {
        int spawnIndex = Random.Range(0, prefab.Length);
        Instantiate(prefab[spawnIndex], transform.position, Quaternion.identity);
    }
}
