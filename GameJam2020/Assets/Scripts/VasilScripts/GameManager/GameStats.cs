using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public delegate void EndWave();
    public static event EndWave OnWaveEnd;

    [SerializeField]
    private int _maxEnemiesAtOnce;
    [SerializeField]
    private int _amountOfEnemiesInFirstWave;
    [SerializeField]
    private Vector2 _xPercentAmountOfEnemiesInWaveIncreasePerYWave;
    [SerializeField]
    private float _timeBetweenWaves;

    private static int _currentWave = 1;
    private List<GameObject> _enemiesInLevel = new List<GameObject>();
    private int _remainingEnemiesInWave;
    private float _nextWaveTimer;

    private bool _waveHasEnded = false;
    private bool _waveInProgress = false;

    private void Start()
    {
        _remainingEnemiesInWave = _amountOfEnemiesInFirstWave;
        _nextWaveTimer = _timeBetweenWaves;
        OnWaveEnd += OnWaveEnded;
    }

    private void Update()
    {
        if(AreAllEnemiesDead() && !_waveHasEnded)
        {
            if(OnWaveEnd != null)
                OnWaveEnd();
        }
        else if(!_waveInProgress)
        {
            _nextWaveTimer -= Time.deltaTime;
            if(_nextWaveTimer <= 0)
                NextWave();
        }
    }

    public void NextWave()
    {
        if(AreAllEnemiesDead())
        {
            InitiateWave(_currentWave + 1);
        }
    }

    public void ForceNextWave()
    {
        KillAllEnemies();

        if(OnWaveEnd != null)
            OnWaveEnd();

        NextWave();
    }

    public bool AreAllEnemiesDead()
    {
        if(_remainingEnemiesInWave <= 0 && _enemiesInLevel.Count <= 0)
            return true;

        return false;
    }

    public void InitiateWave(int pWaveIndex)
    {
        _currentWave = pWaveIndex;
        _remainingEnemiesInWave = Mathf.FloorToInt(_amountOfEnemiesInFirstWave * (1 + (_xPercentAmountOfEnemiesInWaveIncreasePerYWave.x * 0.01f * Mathf.FloorToInt(_currentWave / _xPercentAmountOfEnemiesInWaveIncreasePerYWave.y))));
        _waveHasEnded = false;
        _waveInProgress = true;
    }

    private void OnWaveEnded()
    {
        _nextWaveTimer = _timeBetweenWaves;
        _waveHasEnded = true;
        _waveInProgress = false;
    }

    private void KillAllEnemies()
    {
        _remainingEnemiesInWave = 0;
        foreach(GameObject enemy in _enemiesInLevel)
        {
            enemy.GetComponent<AIBehavior>().TriggerDeath();
        }
    }

    public int MaxEnemiesAtOnce { get => _maxEnemiesAtOnce; }
    public static int CurrentWave { get => _currentWave; }
    public List<GameObject> EnemiesInLevel { get => _enemiesInLevel; }
    public int RemainingEnemiesInWave { get => _remainingEnemiesInWave; set => _remainingEnemiesInWave = value; }
}
