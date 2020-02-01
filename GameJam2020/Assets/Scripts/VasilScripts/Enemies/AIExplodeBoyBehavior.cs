using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIExplodeBoyBehavior : AIBehavior
{
    private SphereCollider _deathCollider;
    private int _shouldDieInFrames = 2;

    protected override void Start()
    {
        base.Start();
        SphereCollider[] cols = GetComponents<SphereCollider>();
        if(cols.Length > 0)
        {
            for(int i = 0; i < cols.Length; i++)
            {
                if(cols[i].isTrigger)
                {
                    _deathCollider = cols[i];
                    _deathCollider.enabled = false;
                    break;
                }
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if(_shouldDie)
        {
            Explode();
        }

        switch(GetAIState())
        {
            case AIState.Attacking:
                Explode();
                break;
        }
    }

    private void Explode()
    {
        //Play explode animation and destroy afterwards
        if(!GetComponent<Collider>().isTrigger)
        {
            GetComponent<Collider>().enabled = false;
        }
        transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 10;
        if(transform.localScale.x >= 3)
        {
            _deathCollider.enabled = true;
            if(_shouldDieInFrames == 0)
                Destroy(this.gameObject);
            _shouldDieInFrames--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AIBehavior>())
        {
            if(_deathCollider.enabled)
                other.gameObject.GetComponent<AIBehavior>().TakeDamage(_enemyStats.Damage / 2, 100);
        }
        else if(other.GetComponent<IDamageable>() != null)
        {
            if(_deathCollider.enabled)
                other.gameObject.GetComponent<IDamageable>().TakeDamage(_enemyStats.Damage);
        }
    }
}
