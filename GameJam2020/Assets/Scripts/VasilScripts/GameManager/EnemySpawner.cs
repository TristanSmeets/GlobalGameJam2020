using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("\t\t\tSpawn Distance from Player")]
    [SerializeField]
    private int _radius;
    [SerializeField]
    private float _maxTimeBetweenEnemySpawns;

    private GameObject[] _enemyPrefabs;
    private Transform _playerTransform;
    private GameStats _gameStats;

    private float _enemySpawnTimer = 0;

    void Start()
    {
        _enemyPrefabs = Resources.LoadAll<GameObject>("Enemies/");
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _gameStats = GetComponent<GameStats>();
    }

    void Update()
    {
        SpawnEnemyAroundPlayer();
    }

    void SpawnEnemyAroundPlayer()
    {
        if(_gameStats.MaxEnemiesAtOnce <= _gameStats.EnemiesInLevel.Count || _gameStats.RemainingEnemiesInWave <= 0)
            return;

        _enemySpawnTimer -= Time.deltaTime;
        if(_enemySpawnTimer > 0)
            return;

        int randEnemy = Random.Range(0, _enemyPrefabs.Length);
        GameObject go = Instantiate(_enemyPrefabs[randEnemy]);
        int point = Random.Range(0, 100);
        Vector3 spawnPosition = new Vector3(_playerTransform.position.x + _radius * Mathf.Cos(point), _playerTransform.position.y, _playerTransform.position.z + _radius * Mathf.Sin(point));
        RaycastHit hit;
        while(!Physics.Raycast(spawnPosition, Vector3.down, out hit, 5))
        {
            point = Random.Range(0, 100);
            spawnPosition = new Vector3(_playerTransform.position.x + _radius * Mathf.Cos(point), _playerTransform.position.y, _playerTransform.position.z + _radius * Mathf.Sin(point));
        }
        go.transform.position = spawnPosition;
        go.transform.LookAt(_playerTransform);

        _enemySpawnTimer = Random.Range(0.05f, _maxTimeBetweenEnemySpawns);
    }
}
