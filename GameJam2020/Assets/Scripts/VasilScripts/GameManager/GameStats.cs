using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField]
    private int _maxEnemiesAtOnce;
    [SerializeField]
    private int _amountOfEnemiesInFirstWave;

    private int _currentWave = 1;
    private List<GameObject> _enemiesInLevel = new List<GameObject>();
    private int _remainingEnemiesInWave;

    private void Start()
    {
        _remainingEnemiesInWave = _amountOfEnemiesInFirstWave;
    }

    public int MaxEnemiesAtOnce { get => _maxEnemiesAtOnce; }
    public int CurrentWave { get => _currentWave; }
    public List<GameObject> EnemiesInLevel { get => _enemiesInLevel; }
    public int RemainingEnemiesInWave { get => _remainingEnemiesInWave; set => _remainingEnemiesInWave = value; }
}
