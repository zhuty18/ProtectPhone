using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float spawnRate = 1.0f;  // Number of enemies spawned per second
    public float spawnRateIncreasePerSec = 0.4f;
    public float maxSpawnRate = 10.0f;

    public GameObject target;
    public Enemy enemy;

    public PathFinderPlatformer pathFinder;

    private float spawnPause;
    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
        InitPathFinder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawning() {
        // Debug.Log("start spawning");
        StartCoroutine(SpawnEnemy(1.0f));
        InvokeRepeating("IncreaseSpawnRate", 0, 1.0f);
    }

    void IncreaseSpawnRate() {
        if (spawnRate < maxSpawnRate) {
            spawnRate += spawnRateIncreasePerSec;
            spawnPause = 1 / spawnRate;
        }
    }

    IEnumerator SpawnEnemy(float delay) {
        yield return new WaitForSeconds(delay);
        while (true) {
            Enemy e = Instantiate(enemy, transform);
            e.SetTarget(target);
            e.SetPathFinder(pathFinder);
            Debug.Log("spawned", e);
            yield return new WaitForSeconds(spawnPause);
        }
    }

    public void InitPathFinder() {
        pathFinder.Init(enemy.width, enemy.height, enemy.moveSpeed, enemy.jumpForce, enemy.gravity);
    }
}
