using UnityEngine;

using UnityEngine.UI;

[System.Serializable]

public class Wave

{

    public string waveName;

    public int noOfEnemies;

    public GameObject[] typeOfEnemies;

    public float spawnInterval;

}

public class WaveSpawnerTest : MonoBehaviour

{

    public Wave[] waves;

    public Transform[] spawnPoints;

    public GameObject[] totalEnemies;

    public Animator animator;

    public Text waveName;

    public Wave currentWave;

    public int currentWaveNumber;

    private float nextSpawnTime;

    private bool canSpawn = true;


    private void Update()

    {

        currentWave = waves[currentWaveNumber];

        SpawnWave();

        totalEnemies = GameObject.FindGameObjectsWithTag("AI");

        if (totalEnemies.Length == 0  )
        {
            if ( currentWaveNumber + 1 != waves.Length )

            {
                SpawnNextWave();
            }
        }
    }

    private void SpawnNextWave()
    {
        currentWaveNumber++;

        canSpawn = true;
    }

    private void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)

        {

            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];

            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);

            currentWave.noOfEnemies--;

            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
            }

        }

        

    }

}