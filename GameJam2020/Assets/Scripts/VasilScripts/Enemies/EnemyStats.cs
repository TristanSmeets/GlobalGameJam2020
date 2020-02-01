using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("\t\t\t\tHealth Settings")]
    private int _baseHealth;
    private int _baseStunHealth;
    [Header("\t\t\t\tAttack Settings")]
    private int _baseDamage;
    private float _attackRange;
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
    public float AttackRange { get => _attackRange; }
    public float WaitTimeAfterAttack { get => _waitTimeAfterAttack; }
}
