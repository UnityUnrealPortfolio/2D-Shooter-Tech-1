using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    [SerializeField] EnemyShipPool shipPool;

    [Tooltip("time in seconds before starting to spawn ships")]
    [SerializeField] float spawnStartTime;

    [Tooltip("rate of ship spawning, if lower, more ships pa unit time")]
    [SerializeField] float spawnIntervalTime;

    [SerializeField][Range(5f,15f)] float maxSpawnRadius;

    [SerializeField] ParticleSystem spawnFx;
    private void Start()
    {
        InvokeRepeating("SpawnEnemyShip",spawnStartTime,spawnIntervalTime);
    }

    private void SpawnEnemyShip()
    {
        //get a ship from the pool
        GameObject spawnedShip = shipPool.GetPoolObject();

        //set it to a random position within a sphere
        float randomXPos = Random.Range( -maxSpawnRadius,maxSpawnRadius);
        float randomYPos = Random.Range(-maxSpawnRadius, maxSpawnRadius);

        Vector2 spawnPos = new Vector2(randomXPos,randomYPos);
        spawnedShip.transform.position = spawnPos;
        
        var fx = Instantiate(spawnFx,spawnedShip.transform.position,Quaternion.identity);
        Destroy(fx, 2f);
    }
}
