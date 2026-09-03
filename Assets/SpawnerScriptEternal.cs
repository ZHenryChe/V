using UnityEngine;
using System;

public class SpawnerScriptEternal : MonoBehaviour
{
    public Phase1ManagerScript managerScript;

    public GameObject player;
    public GameObject platform;
    public GameObject objectToSpawn1;
    public GameObject objectToSpawn2;

    public float platformSpawnDelay = 2.0f;
    public float platformSpawnTimer = 0.0f;
    public float enemySpawnDelay = 1.5f;
    public float enemySpawnTimer = 0.0f;

    public float accelerationFactor = 0.001f;

    private float enemySpawnPos = 0.0f;

    public void SpawnDragoon(float xPosition)
    {
        Instantiate(objectToSpawn1, new Vector3(xPosition, 1, 10), Quaternion.identity);
    }

    public void SpawnPlatform(bool execute, float deltaPos = 600, bool reset = true, bool enemy = true)
    {
        float deltaTime = Time.deltaTime;

        if (platformSpawnTimer < platformSpawnDelay && !execute)
        {
            platformSpawnTimer += deltaTime;
            enemySpawnTimer += deltaTime;
        }
        else
        {
            Vector3 playerPosition = player.transform.position;
            float randomDistanceOffset = UnityEngine.Random.Range(-20.0f, 20.0f);

            if (enemy && enemySpawnTimer > enemySpawnDelay && Math.Abs(playerPosition.x + deltaPos + randomDistanceOffset - enemySpawnPos) > 100)
            {
                int randomObstacle = UnityEngine.Random.Range(0, 3);

                enemySpawnPos = playerPosition.x + deltaPos + randomDistanceOffset;

                if (randomObstacle == 0)
                {
                    Instantiate(objectToSpawn1, new Vector3(enemySpawnPos, 1, 10), Quaternion.identity);
                }
                else if (randomObstacle == 1)
                {
                    Instantiate(objectToSpawn1, new Vector3(enemySpawnPos, 1, 10), Quaternion.identity);
                    Instantiate(objectToSpawn1, new Vector3(enemySpawnPos + 10, 1, 10), Quaternion.identity);
                }
                else
                {
                    Instantiate(objectToSpawn2, new Vector3(playerPosition.x + deltaPos + randomDistanceOffset, 7, 10), Quaternion.identity);
                }
                enemySpawnTimer = 0.0f;
            }
            else
            {
                enemySpawnTimer += deltaTime;
            }
            Instantiate(platform, new Vector3(playerPosition.x + deltaPos, -21, 0), Quaternion.identity);
            if (reset)
            {
                platformSpawnTimer = 0.0f;
            }
            //Debug.Log("enemytimer: " + enemySpawnTimer);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPlatform(true, 0, enemy: false);
        SpawnDragoon(150);
        SpawnPlatform(true, 150, enemy: false);
        SpawnPlatform(true, 300, enemy: false);
        SpawnPlatform(true, 450, enemy: false);
        SpawnPlatform(true, 600, enemy: false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!managerScript.phase1Start)
        {
            enemySpawnPos -= 100f / managerScript.platformSpawnInterval * Time.deltaTime;
            //Debug.Log("Expected enemy spawn position: " + (player.transform.position.x + 300));
            //Debug.Log("enemySpawnPos: " + enemySpawnPos);

            platformSpawnDelay = managerScript.platformSpawnInterval;
            enemySpawnDelay = managerScript.platformSpawnInterval * UnityEngine.Random.Range(0.7f, 0.9f);
            if (enemySpawnDelay < 1f)
            {
                enemySpawnDelay = 1f;
            }
            //Debug.Log("enemySpawnDelay: " + enemySpawnDelay);

            if (managerScript.gameTimer * accelerationFactor < 1.5f)
            {
                managerScript.platformSpawnInterval = 2.0f - managerScript.gameTimer * accelerationFactor;
            }
            //Debug.Log("Spawn Interval: " + spawnInterval);

            SpawnPlatform(false);
        }
        else
        {
            SpawnPlatform(false, enemy: false);
        }

    }
}
