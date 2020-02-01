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

        if(_shouldDie)
            Destroy(this.gameObject);

        switch(GetAIState())
        {
            case AIState.Attacking:
                //Play attack animation and Wait() after it's finished
                Wait(_enemyStats.WaitTimeAfterAttack);
                break;
        }
    }
}
