using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveToTarget : MonoBehaviour
{
    private Transform _targetTransform;
    private NavMeshAgent _navMeshAgent;
    private AIBehavior _aiBehavior;

    void Start()
    {
        _targetTransform = GameObject.Find("Player").GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _aiBehavior = GetComponent<AIBehavior>();

        _navMeshAgent.stoppingDistance = GetComponent<Collider>().bounds.extents.x + _targetTransform.gameObject.GetComponent<Collider>().bounds.extents.x * 2;
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
}
