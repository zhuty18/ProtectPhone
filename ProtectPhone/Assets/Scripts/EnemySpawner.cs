using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float spawnRate = 1.0f;  // Number of enemies spawned per second

    public GameObject enemy;

    private float spawnPause;
    // Start is called before the first frame update
    void Start()
    {
        spawnPause = 1 / spawnRate;
        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawning() {
        InvokeRepeating("SpawnEnemy", spawnPause, spawnPause);
    }

    void SpawnEnemy() {
        GameObject spawned = Instantiate(enemy, transform);
        Debug.Log("spawned", spawned);
    }
}
