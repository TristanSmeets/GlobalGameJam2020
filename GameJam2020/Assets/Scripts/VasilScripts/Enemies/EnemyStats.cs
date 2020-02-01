using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [Header("\t\t\t\tHealth Settings")]
    [SerializeField]
    private int _baseHealth;
    [SerializeField]
    private int _baseStunHealth;
    [SerializeField]
    private float _baseMovementSpeed;
    [Header("\t\t\tDefensive Stat Growth")]
    [SerializeField]
    private Vector2 _xPercentHealthIncreaseiPerYWaves;
    [SerializeField]
    private Vector2 _xPercentStunHealthIncreasePerYWaves;
    [SerializeField]
    private Vector2 _xPercentMovementSpeedIncreasePerYWaves;

    [Header("\t\t\t\tAttack Settings")]
    [SerializeField]
    private int _baseDamage;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _waitTimeAfterAttack;
    [Header("\t\t\tOffensive Stat Growth")]
    [SerializeField]
    private Vector2 _xPercentDamageIncreasePerYWaves;
    [SerializeField]
    private Vector2 _xPercentAttackSpeedIncreasePerYWaves;

    private int _currentWave;
    private int _currentHealth;
    private int _currentStunHealth;
    private float _movementSpeed;
    private int _damage;
    private float _attackRecovery;

    private void Start()
    {
        InitializeStats();
    }

    public void InitializeStats()
    {
        _currentWave = GameStats.CurrentWave;
        _currentHealth = Mathf.FloorToInt(_baseHealth * (1 + (_xPercentHealthIncreaseiPerYWaves.x * 0.01f * Mathf.FloorToInt(_currentWave / _xPercentHealthIncreaseiPerYWaves.y))));
        _currentStunHealth = Mathf.FloorToInt(_baseStunHealth * (1 + _xPercentStunHealthIncreasePerYWaves.x * 0.01f * Mathf.FloorToInt(_currentWave / _xPercentStunHealthIncreasePerYWaves.y)));
        _movementSpeed = _baseMovementSpeed * (1 + _xPercentMovementSpeedIncreasePerYWaves.x * 0.01f * Mathf.FloorToInt(_currentWave / _xPercentMovementSpeedIncreasePerYWaves.y));
        GetComponent<NavMeshAgent>().speed = _movementSpeed;
        GetComponent<NavMeshAgent>().acceleration = _movementSpeed * 10;
        _damage = Mathf.FloorToInt(_baseDamage * (1 + (_xPercentDamageIncreasePerYWaves.x * 0.01f * Mathf.FloorToInt(_currentWave / _xPercentDamageIncreasePerYWaves.y))));
        _attackRecovery = _waitTimeAfterAttack / (1 + (_xPercentAttackSpeedIncreasePerYWaves.x * 0.01f * Mathf.FloorToInt(_currentWave / _xPercentAttackSpeedIncreasePerYWaves.y)));
    }

    public int MaxHealth { get => _baseHealth; }
    public int MaxStunHealth { get => _baseStunHealth; }
    public int Damage { get => _damage; }
    public float AttackRange
    {
        get => _attackRange;
        set
        {
            _attackRange = value;
            GetComponent<NavMeshAgent>().stoppingDistance = _attackRange;
        }
    }
    public float WaitTimeAfterAttack { get => _attackRecovery; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int CurrentStunHealth { get => _currentStunHealth; set => _currentStunHealth = value; }
    public Vector2 XPercentAttackSpeedIncreasePerYWaves { get => _xPercentAttackSpeedIncreasePerYWaves; }
}