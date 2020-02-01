using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveToTarget : MonoBehaviour
{
    private Transform _targetTransform;
    private NavMeshAgent _navMeshAgent;
    private AIBehavior _aiBehavior;
    private EnemyStats _enemyStats;

    void Start()
    {
        _targetTransform = GameObject.Find("Player").GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _aiBehavior = GetComponent<AIBehavior>();
        _enemyStats = GetComponent<EnemyStats>();

        _navMeshAgent.stoppingDistance = _enemyStats.AttackRange;
    }

    void Update()
    {
        switch(_aiBehavior.GetAIState())
        {
            case AIBehavior.AIState.Chasing:
                _navMeshAgent.destination = _targetTransform.position;
                break;
        }
    }

    public Vector3 GetVelocity() { if(_navMeshAgent) return _navMeshAgent.velocity; else return Vector3.zero; }
    public float GetDistanceToTarget() { return Vector3.Distance(transform.position, _targetTransform.position); }
    public Transform TargetTransform { get => _targetTransform; }
}
