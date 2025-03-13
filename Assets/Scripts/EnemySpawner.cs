using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs;
    private Transform player;
    public float minSpawnRadius = 30f;
    public float maxSpawnRadius = 40f;
    public float spawnInterval = 7f;    // Seconds between each enemy spawn
    public LayerMask groundLayer;
    
    private void Start()
    {
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            else
            {
                Debug.LogError("EnemySpawner: No player instance with the tag 'Player' was found in the scene!");
            }
        }

        StartCoroutine(SpawnEnemies());
    }
    
    private IEnumerator SpawnEnemies()
    {
        yield return new WaitUntil(() => player != null);
        
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || player == null) return;

        Vector3 spawnPosition = default;
        bool validPosition = false;

        // Here we find a valid position to spawn our enemies
        int attempts = 10;
        while (!validPosition && attempts > 0)
        {
            attempts--;
            Vector3 randomDirection = Random.insideUnitSphere * maxSpawnRadius;
            randomDirection.y = 0;
            spawnPosition = player.position + randomDirection;

            float distanceToPlayer = Vector3.Distance(spawnPosition, player.position);

            // Not to close to the player and not too far (will look better with fog)
            if (distanceToPlayer > minSpawnRadius && distanceToPlayer < maxSpawnRadius)
            {
                if (Physics.Raycast(spawnPosition + Vector3.up * 10, Vector3.down, out RaycastHit hit, 20f,
                        groundLayer))
                {
                    spawnPosition = hit.point;
                    validPosition = true;
                }
            }
        }

        if (validPosition)
        {
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (player == null) return;

        // Invalid spawn zone
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(player.position, minSpawnRadius);

        // Valid spawn zone
        Gizmos.color = new Color(0,1,0,0.3f);
        Gizmos.DrawWireSphere(player.position, maxSpawnRadius);
    }

}
