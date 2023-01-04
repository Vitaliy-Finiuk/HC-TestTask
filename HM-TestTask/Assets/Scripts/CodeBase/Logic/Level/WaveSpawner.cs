using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySetup> _enemies = new List<EnemySetup>();
    [SerializeField] private int _currentWave;
    [SerializeField] private int _waveValue;
    [SerializeField] private List<GameObject> _enemiesToSpawn = new List<GameObject>();

    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private int _waveDuration;
    [SerializeField] private float _waveTimer;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnTimer;

    private void Start()
    {
        GenerateWave();
    }

    private void FixedUpdate()
    {
        if (_spawnTimer <= 0)
        {
            if (_enemiesToSpawn.Count > 0)
            {
                Instantiate(_enemiesToSpawn[0], _spawnLocation.position, Quaternion.identity);
                _enemiesToSpawn.RemoveAt(0);
                _spawnTimer = _spawnInterval;
            }
            else
            {
                _waveTimer = 0;
            }
        }
        else
        {
            _spawnTimer -= Time.fixedDeltaTime;
            _waveTimer -= Time.fixedDeltaTime;
        }
    }


    public void GenerateWave()
    {
        _waveValue = _currentWave * 10;
        GenerateEnemies();

        _spawnInterval = _waveDuration / _enemiesToSpawn.Count;
        _waveTimer = _waveDuration;
    }

    private void GenerateEnemies()
    {
        List<GameObject> _generateEnemies = new List<GameObject>();

        while (_waveValue > 0)
        {
            int randEnemyId = Random.Range(0, _enemies.Count);
            int randEnemyCost = _enemies[randEnemyId].Cost;

            if (_waveValue - randEnemyCost >=0)
            {
                _generateEnemies.Add(_enemies[randEnemyId].EnemyPrefab);
                _waveValue -= randEnemyCost;
            }else if (_waveValue <= 0)
            {
                break;
            }
        }
        _enemiesToSpawn.Clear();
        _enemiesToSpawn = _generateEnemies;
    }
}

[System.Serializable]
public class EnemySetup
{
    public GameObject EnemyPrefab;
    public int Cost;
}
