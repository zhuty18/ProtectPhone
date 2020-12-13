using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float spawnRate = 0.05f;  // Number of enemies spawned per second
    public float spawnRateIncreasePerSec = 0.01f;
    public float maxSpawnRate = 10.0f;
    public int maxEnemyCount = 16;

    public GameObject target;
    public Enemy enemy;

    // public PathFinderPlatformer pathFinder;

    private static HashSet<Enemy> enemies = new HashSet<Enemy>();

    private float spawnPause;
    // Start is called before the first frame update
    void Start()
    {
        // maxEnemyCount = 3;
        StartSpawning();
        InitPathFinder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawning() {
        Debug.Log("start spawning");
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
            if (target != null && enemies.Count < maxEnemyCount) {
                Enemy e = Instantiate(enemy, transform);
                e.SetTarget(target);
                // e.SetPathFinder(pathFinder);
                e.SetEnemySpawner(this);
                enemies.Add(e);
                // Debug.Log("spawned", e);
            }
            yield return new WaitForSeconds(spawnPause);
        }
    }

    public void OnEnemyDestroy(Enemy e) {
        enemies.Remove(e);
    }

    public bool SpawnBlocked() {
        if (Vector2.Distance(target.transform.position, transform.position) < 1.2f) {
            return true;
        }
        foreach (Enemy enemy in enemies) {
            if (Vector2.Distance(enemy.transform.position, transform. position) < 1.2f) {
                return true;
            }
        }
        return false;
    }

    public void InitPathFinder() {
        // pathFinder.Init(enemy.width, enemy.height, enemy.moveSpeed, enemy.jumpForce, enemy.gravity);
    }
}
