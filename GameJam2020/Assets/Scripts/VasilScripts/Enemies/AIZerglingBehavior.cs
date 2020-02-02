using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZerglingBehavior : AIBehavior
{
    [SerializeField]
    private float _preparationForAttackDuration;
    [SerializeField]
    private float _normalAttackRange;
    [SerializeField]
    private float _jumpCooldown;
    [SerializeField]
    private float _jumpAttackRange;

    private float _animationTime = 0.5f;
    private float _preparationForAttackTimer;
    private float _jumpTimer;
    private bool _gotJumpPos;
    private Vector3 _jumpToPos;

    protected override void Start()
    {
        base.Start();

        _enemyStats.AttackRange = _jumpAttackRange;
        _preparationForAttackDuration /= 1 + (_enemyStats.XPercentAttackSpeedIncreasePerYWaves.x * 0.01f * Mathf.FloorToInt(GameStats.CurrentWave / _enemyStats.XPercentAttackSpeedIncreasePerYWaves.y));
        _preparationForAttackTimer = _preparationForAttackDuration;
    }

    protected override void Update()
    {
        base.Update();

        if(_shouldDie)
            Destroy(this.gameObject);

        _jumpTimer -= Time.deltaTime;
        if(_jumpTimer <= 0)
            _enemyStats.AttackRange = _jumpAttackRange;

        switch(GetAIState())
        {
            case AIState.Attacking:
                _preparationForAttackTimer -= Time.deltaTime;
                if(_preparationForAttackTimer <= 0)
                {
                    _animationTime -= Time.deltaTime;
                    if(_jumpTimer <= 0 && !_gotJumpPos && _animationTime > 0.25f)
                    {
                        if(_aiMoveToTarget.GetDistanceToTarget() > _enemyStats.AttackRange * 0.25f)
                        {
                            _jumpToPos = transform.position + transform.forward * (_aiMoveToTarget.GetDistanceToTarget() -
                                                                                   GetComponent<Collider>().bounds.extents.x -
                                                                                   _aiMoveToTarget.TargetTransform.GetComponent<Collider>().bounds.extents.x * 2);
                            _gotJumpPos = true;
                            _enemyStats.AttackRange = _normalAttackRange;
                        }
                    }

                    if(_gotJumpPos)
                    {
                        transform.position = Vector3.Slerp(transform.position, _jumpToPos, Time.deltaTime * 10);
                        _jumpTimer = _jumpCooldown;
                    }

                    if(_animationTime <= 0.1f)
                    {
                        _normalAttackCollider.enabled = true;
                        if(!GetComponentInChildren<Animation>().isPlaying)
                            GetComponentInChildren<Animation>().Play();
                    }
                }

                if(_animationTime > 0)
                    return;

                _preparationForAttackTimer = _preparationForAttackDuration;
                _animationTime = 0.5f;
                _gotJumpPos = false;
                _normalAttackCollider.enabled = false;
                Wait(_enemyStats.WaitTimeAfterAttack);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            if(_normalAttackCollider.enabled)
            {
                other.GetComponent<IDamageable>().TakeDamage(_enemyStats.Damage);
            }
        }
    }
}
