using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZerglingBehavior : AIBehavior
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        switch(GetAIState())
        {
            case AIState.Chasing:
                if(_aiMoveToTarget.GetDistanceToTarget() <= _enemyStats.AttackRange)
                {
                    if(_aiMoveToTarget.GetVelocity().magnitude <= 1)
                    {
                        Attack();
                    }
                }
                break;
        }
    }
}
