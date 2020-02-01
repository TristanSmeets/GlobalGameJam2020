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
    [Header("\t\t\t\tAttack Settings")]
    [SerializeField]
    private int _baseDamage;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _waitTimeAfterAttack;

    [HideInInspector]
    public int CurrentHealth;
    [HideInInspector]
    public int CurrentStunHealth;

    private void Start()
    {
        CurrentHealth = _baseHealth;
        CurrentStunHealth = _baseStunHealth;
    }

    public int MaxHealth { get => _baseHealth; }
    public int MaxStunHealth { get => _baseStunHealth; }
    public int Damage { get => _baseDamage; }
    public float AttackRange
    {
        get => _attackRange;
        set
        {
            _attackRange = value;
            GetComponent<NavMeshAgent>().stoppingDistance = _attackRange;
        }
    }
    public float WaitTimeAfterAttack { get => _waitTimeAfterAttack; }
}