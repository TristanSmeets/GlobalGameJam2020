using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZerglingBehavior : AIBehavior
{
    [SerializeField]
    private float _preparationForAttackDuration;
    [SerializeField]
    private int _normalAttackRange;
    [SerializeField]
    private float _jumpCooldown;
    [SerializeField]
    private int _jumpAttackRange;

    private float _animationTime = 1;
    private float _preparationForAttackTimer;
    private float _jumpTimer;
    private bool _gotJumpPos;
    private Vector3 _jumpToPos;

    protected override void Start()
    {
        base.Start();
        _preparationForAttackTimer = _preparationForAttackDuration;
        _enemyStats.AttackRange = _jumpAttackRange;
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
                    //Play attack animation and Wait() after it's finished
                    _animationTime -= Time.deltaTime;

                    if(_jumpTimer <= 0 && !_gotJumpPos && _animationTime > 0.5f)
                    {
                        if(_aiMoveToTarget.GetDistanceToTarget() > _enemyStats.AttackRange * 0.5f)
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
                }

                if(_animationTime > 0)
                    return;

                _preparationForAttackTimer = _preparationForAttackDuration;
                _animationTime = 1;
                _gotJumpPos = false;
                Wait(_enemyStats.WaitTimeAfterAttack);
                break;
        }
    }
}
