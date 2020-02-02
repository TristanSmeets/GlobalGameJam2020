using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public delegate void StartWave();
    public static event StartWave OnWaveStart;
    public delegate void EndWave();
    public static event EndWave OnWaveEnd;

    public delegate void StartRound();
    public static event StartRound OnRoundStart;
    public delegate void EndRound();
    public static event EndRound OnRoundEnd;

    [SerializeField]
    private int _wavesPerRound;
    [SerializeField]
    private int _maxEnemiesAtOnce;
    [SerializeField]
    private int _amountOfEnemiesInFirstWave;
    [SerializeField]
    private Vector2 _xPercentAmountOfEnemiesInWaveIncreasePerYWave;
    [SerializeField]
    private float _timeBetweenWaves;

    private static int _currentWave;
    private static int _currentRound;
    private List<GameObject> _enemiesInLevel = new List<GameObject>();
    private int _remainingEnemiesInWave;
    private float _nextWaveTimer;

    private bool _waveHasEnded;
    private bool _waveInProgress;
    private bool _roundHasEnded;
    private bool _roundInProgress;

    private void Start()
    {
        OnWaveEnd += OnWaveEnded;
        UpgradeScreen.StartNextRound += NextRound;
        _roundHasEnded = true;
        _waveHasEnded = true;
    }

    private void OnDestroy()
    {
        OnWaveEnd -= OnWaveEnded;
        UpgradeScreen.StartNextRound -= NextRound;
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && !_roundInProgress && _roundHasEnded)
        {
            if(OnRoundStart != null)
                OnRoundStart();
            NextRound();
        }

        if(!_roundInProgress)
            return;

        if(AreAllEnemiesDead() && !_waveHasEnded)
        {
            if(OnWaveEnd != null)
                OnWaveEnd();
        }
        else if(!_waveInProgress)
        {
            _nextWaveTimer -= Time.deltaTime;
            if(_nextWaveTimer <= 0)
            {
                NextWave();
            }
        }
    }

    public void NextRound()
    {
        InitiateRound(_currentRound + 1);
    }

    public void InitiateRound(int pRoundIndex)
    {
        _currentRound = pRoundIndex;
        _nextWaveTimer = _timeBetweenWaves;
        _currentWave = 0;
        _roundInProgress = true;
        _roundHasEnded = false;

        if(OnRoundStart != null)
            OnRoundStart();

        NextWave();
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

        if(OnWaveStart != null)
            OnWaveStart();
    }

    private void OnWaveEnded()
    {
        _nextWaveTimer = _timeBetweenWaves;
        _waveHasEnded = true;
        _waveInProgress = false;

        if(_currentWave == _wavesPerRound)
        {
            _roundHasEnded = true;
            _roundInProgress = false;

            if(OnRoundEnd != null)
                OnRoundEnd();
        }
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
    public static int CurrentRound { get => _currentRound; }
    public List<GameObject> EnemiesInLevel { get => _enemiesInLevel; }
    public int RemainingEnemiesInWave { get => _remainingEnemiesInWave; set => _remainingEnemiesInWave = value; }
}
