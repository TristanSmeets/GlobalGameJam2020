using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIBehavior : MonoBehaviour
{
    public enum AIState
    {
        Waiting,
        Chasing,
        Attacking,
        Stunned
    }

    private AIState _currentAIState;

    protected AIMoveToTarget _aiMoveToTarget;
    protected NavMeshAgent _navMeshAgent;
    protected EnemyStats _enemyStats;
    protected GameStats _gameStats;

    protected bool _isWaiting = false;
    protected bool _isChasing = false;
    protected bool _isAttacking = false;
    protected bool _isStunned = false;
    protected bool _shouldDie;

    private float _waitTimer;
    private float _stunTimer;

    protected virtual void Start()
    {
        _currentAIState = AIState.Waiting;

        _aiMoveToTarget = GetComponent<AIMoveToTarget>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStats = GetComponent<EnemyStats>();
        _gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();

        _gameStats.EnemiesInLevel.Add(this.gameObject);
        _gameStats.RemainingEnemiesInWave--;
    }

    protected virtual void Update()
    {
        switch(_currentAIState)
        {
            case AIState.Waiting:
                _waitTimer -= Time.deltaTime;
                if(_waitTimer <= 0)
                {
                    _currentAIState = AIState.Chasing;
                }
                break;

            case AIState.Chasing:
                if(_aiMoveToTarget.GetDistanceToTarget() <= _enemyStats.AttackRange)
                {
                    if(_aiMoveToTarget.GetVelocity().magnitude <= 1)
                    {
                        Attack();
                        transform.LookAt(new Vector3(_aiMoveToTarget.TargetTransform.position.x, transform.position.y, _aiMoveToTarget.TargetTransform.transform.position.z));
                    }
                }
                break;

            case AIState.Attacking:
                break;

            case AIState.Stunned:
                _stunTimer -= Time.deltaTime;
                if(_stunTimer <= 0)
                {
                    _currentAIState = AIState.Chasing;
                }
                break;
        }
    }

    protected virtual void Wait(float pWaitDuration)
    {
        _currentAIState = AIState.Waiting;
        _waitTimer = pWaitDuration;
    }

    protected virtual void Chase()
    {
        _currentAIState = AIState.Chasing;
    }

    protected virtual void Attack()
    {
        _currentAIState = AIState.Attacking;
    }

    protected virtual void Stun(float pStunDuration)
    {
        _currentAIState = AIState.Stunned;
        _stunTimer = pStunDuration;
    }

    public virtual void TakeDamage(int pHealthDamage, int pStunDamage)
    {
        _enemyStats.CurrentHealth -= pHealthDamage;
        _enemyStats.CurrentStunHealth -= pStunDamage;
        if(_enemyStats.CurrentHealth <= 0)
        {
            _shouldDie = true;
        }
        else if(_enemyStats.CurrentStunHealth <= 0)
        {
            Stun(0.25f);
            _enemyStats.CurrentStunHealth = _enemyStats.MaxStunHealth;
        }
    }

    protected virtual void OnDestroy()
    {
        _gameStats.EnemiesInLevel.Remove(this.gameObject);
    }

    public AIState GetAIState() { return _currentAIState; }
    public bool GetWaiting() { return _isWaiting; }
    public bool GetChasing() { return _isChasing; }
    public bool GetAttacking() { return _isAttacking; }
    public bool GetStunned() { return _isStunned; }
}
